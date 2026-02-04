using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Rendering;
using UnityEngine;

public class RandomItemBox : MonoBehaviour
{
    [SerializeField] ItemDatas m_itemDatas;

    GameObject m_retentionObject;   // 前のオブジェクト保存用
	Advantageous m_advantageous;

    int m_index;

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
                GameObject itemObject = Instantiate(
					m_itemDatas.m_itemDatas[m_index].m_dropItemPrefabs,
					gameObject.transform.position,
					Quaternion.identity,
					transform);
                m_retentionObject = itemObject;

                if (itemObject.TryGetComponent<Rigidbody>(out var itemObjectRb))
                {
                    itemObjectRb.useGravity = false;
                }

                if (itemObject.TryGetComponent<BoxCollider>(out var itemObjectCol))
                {
                    itemObjectCol.enabled = false;
                }

                if (itemObject.TryGetComponent<CapsuleCollider>(out var itemObjectCapCol))
                {
                    itemObjectCapCol.enabled = false;
                }

                m_index++;
                m_inputTime = Time.time;

                if (m_index >= m_itemDatas.m_itemDatas.Count)
                {
                    m_index = 0;
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

            if (other.TryGetComponent<TakeItem>(out var player))
            {
                // アイテムを持っているときは抽選しない
                if (!player.GetHaveItem())
                {
                    // プレイヤーにアイテムを持たせる
                    player.GettingItem(LotteryItem());
                }
            }
        }
    }

    // アイテムのティアをランダムに選ぶ
    private int TierIndex()
    {
        // Tier1,Tier2,Tier3の重み
        int[] tierWeights = { 1, 5, 19 };

		if(m_advantageous.GetSituation() == Advantageous.Situation.Advantage)
		{
			tierWeights = new int[] { 1, 3, 21};

			Debug.Log(Advantageous.Situation.Advantage);
		}
		else if(m_advantageous.GetSituation() == Advantageous.Situation.SlightAdvantage)
		{
			tierWeights = new int[] { 3, 5, 17};

			Debug.Log(Advantageous.Situation.SlightAdvantage);
		}
		else if (m_advantageous.GetSituation() == Advantageous.Situation.Even)
		{
			tierWeights = new int[] { 5, 7, 13};

			Debug.Log(Advantageous.Situation.Even);
		}
		else if (m_advantageous.GetSituation() == Advantageous.Situation.SlightDisadvantage)
		{
			tierWeights = new int[] { 7, 10, 8};

			Debug.Log(Advantageous.Situation.SlightDisadvantage);
		}
		else if (m_advantageous.GetSituation() == Advantageous.Situation.Disadvantage)
		{
			tierWeights = new int[] { 10, 15, 5};

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

    // アイテムをランダムに選ぶ
    private int LotteryItem()
    {
        int maxTier = 0;

        // データ内の最大ティアを取得
        for (int i = 0; i < m_itemDatas.m_itemDatas.Count; i++)
        {
            if (m_itemDatas.m_itemDatas[i].m_tier > maxTier)
            {
                maxTier = m_itemDatas.m_itemDatas[i].m_tier;
            }
        }

        // ランダムに選んだティア
        int selectedTier = TierIndex();

        List<int> itemsList = new List<int>();

        // 選ばれたティアに属するアイテムのインデックスをリストに追加
        for (int i = 0; i < m_itemDatas.m_itemDatas.Count; i++)
        {
            if (m_itemDatas.m_itemDatas[i].m_tier == selectedTier)
            {
                itemsList.Add(i);
            }
        }

        // 該当ティアのアイテムがない場合は-1を返す(存在しないID)
        if (itemsList.Count == 0)
        {
            return -1;
        }

        // 該当ティアからランダムに1つ選ぶ
        int dataIndex = itemsList[Random.Range(0, itemsList.Count)];
        
        // 選ばれたアイテムのIDを返す
        return m_itemDatas.m_itemDatas[dataIndex].m_itemId;
    }
}
