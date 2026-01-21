using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRandomBox : MonoBehaviour
{
    [SerializeField] WeaponDatas m_weaponDatas;

    GameObject m_retentionObject;   // 前のオブジェクト保存用

    int m_index = 1;

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
                    m_weaponDatas.m_weaponDatas[m_index].m_dropWeaponPrefabs,
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

            if (other.TryGetComponent<TakeWeapon>(out var player))
            {
                // アイテムを持っているときは抽選しない
                if (!player.GetIsHaveWeapon())
                {
                    // ランダムに選ばれたアイテムをプレイヤーに持たせる
                    int itemIndex = Random.Range(0, m_weaponDatas.m_weaponDatas.Count);
                    player.GettingWeapon(itemIndex);
                }
            }
        }
    }
}
