using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRandomBox : MonoBehaviour
{
    [SerializeField] WeaponDatas m_weaponDatas;

    GameObject m_retentionObject;   // 前のオブジェクト保存用
	Advantageous m_advantageous;

    int m_index = 1;    // 0は素手なので1からスタート

    float m_inputTime;
    float m_delay = 0.3f;

    bool m_isGet = false;

    private void Update()
    {
        if (!m_isGet)
        {
            // 一定間隔でインデックスを進める
            if (Time.time - m_inputTime > m_delay)
            {
                // 保存したオブジェクトがあれば削除
                if (m_retentionObject != null)
                {
                    Destroy(m_retentionObject);
                }

                // 新しく生成し保存
                GameObject weaponObject = Instantiate(
                    m_weaponDatas.m_weaponDatas[m_index].m_dropWeaponPrefabs,
                    gameObject.transform.position,
                    Quaternion.identity,
                    transform);
                m_retentionObject = weaponObject;

                if (weaponObject.TryGetComponent<Rigidbody>(out var weaponObjectRb))
                {
                    weaponObjectRb.useGravity = false;
                }

                if (weaponObject.TryGetComponent<BoxCollider>(out var weaponObjectCol))
                {
                    weaponObjectCol.enabled = false;
                }

                m_index++;
                m_inputTime = Time.time;

                if (m_index >= m_weaponDatas.m_weaponDatas.Count)
                {
                    m_index = 1;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_isGet) return;

        if (other.CompareTag("Player"))
        {
            m_isGet = true;
            Destroy(m_retentionObject);

			m_advantageous = other.GetComponent<Advantageous>();

            if (other.TryGetComponent<TakeWeapon>(out var player))
            {
                // アイテムを持っているときは抽選しない
                if (!player.GetIsHaveWeapon())
                {   
                    // プレイヤーに武器を持たせる
                    player.GettingWeapon(LotteryWeapon());
                }
            }
        }
    }

    // 武器のティアをランダムに選ぶ
    private int TierIndex()
    {
		// Tier1,Tier2,Tier3の重み
		int[] tierWeights = { 1, 5, 19 };

		if (m_advantageous.GetSituation() == Advantageous.Situation.Advantage)
		{
			tierWeights = new int[] { 1, 3, 21 };

			Debug.Log(Advantageous.Situation.Advantage);
		}
		else if (m_advantageous.GetSituation() == Advantageous.Situation.SlightAdvantage)
		{
			tierWeights = new int[] { 3, 5, 17 };

			Debug.Log(Advantageous.Situation.SlightAdvantage);
		}
		else if (m_advantageous.GetSituation() == Advantageous.Situation.Even)
		{
			tierWeights = new int[] { 5, 7, 13 };

			Debug.Log(Advantageous.Situation.Even);
		}
		else if (m_advantageous.GetSituation() == Advantageous.Situation.SlightDisadvantage)
		{
			tierWeights = new int[] { 7, 10, 8 };

			Debug.Log(Advantageous.Situation.SlightDisadvantage);
		}
		else if (m_advantageous.GetSituation() == Advantageous.Situation.Disadvantage)
		{
			tierWeights = new int[] { 10, 15, 5 };

			Debug.Log(Advantageous.Situation.Disadvantage);
		}

		// ChooseWeightedは0,1,2を返す
		int index = ChooseWeighted(tierWeights);

        // 1～3のTier番号に変換
        return index + 1;
    }

    // 重みに応じてインデックスを返す
    private int ChooseWeighted(int[] weights)
    {
        int total = 0;

        // 配列の要素を合計して重みの計算
        foreach (int elem in weights)
        {
            total += elem;
        }

        // 0～totalの範囲でランダムに抽選
        int randomPoint = Random.Range(0, total);

        // 重みに応じて抽選
        for (int i = 0; i < weights.Length; i++)
        {
            if (randomPoint < weights[i])
            {
                // 抽選に当たったインデックスを返す
                return i;
            }
            // 当たらなければ残りのポイントで次へ
            randomPoint -= weights[i];
        }

        // 念のため最後の配列のインデックスを返す
        return weights.Length - 1;
    }

    // 武器をランダムに選ぶ
    private int LotteryWeapon()
    {
        int maxTier = 0;

        // データ内の最大ティアを取得
        for (int i = 0; i < m_weaponDatas.m_weaponDatas.Count; i++)
        {
            if (m_weaponDatas.m_weaponDatas[i].m_tier > maxTier)
            {
                maxTier = m_weaponDatas.m_weaponDatas[i].m_tier;
            }
        }

        // ランダムに選んだティア
        int selectedTier = TierIndex();

        List<int> weaponsList = new List<int>();

        // 選ばれたティアに属する武器のインデックスをリストに追加
        for (int i = 0; i < m_weaponDatas.m_weaponDatas.Count; i++)
        {
            if (m_weaponDatas.m_weaponDatas[i].m_tier == selectedTier)
            {
                weaponsList.Add(i);
            }
        }

        // 該当ティアの武器がない場合は0を返す(ID 0は素手)
        if (weaponsList.Count == 0)
        {
            return 0;
        }

        // 該当ティアからランダムに1つ選ぶ
        int dataIndex = weaponsList[Random.Range(0, weaponsList.Count)];

        // 選ばれた武器のIDを返す
        return m_weaponDatas.m_weaponDatas[dataIndex].m_weaponId;
    }
}
