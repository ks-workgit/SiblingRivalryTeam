using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemBox : MonoBehaviour
{
    [SerializeField] ItemDatas m_itemDatas;

    GameObject m_retentionObject;   // 前のオブジェクト保存用

    int m_index;

    float m_inputTime;
    float m_delay = 0.3f;

    bool m_isGet;

    private void Update()
    {
        if (m_isGet) return;

        // 一定間隔でインデックスを進める
        if (Time.time - m_inputTime > m_delay)
        {
            // 保存したオブジェクトがあれば削除
            if (m_retentionObject != null)
            {
                Destroy(m_retentionObject);
            }

            // 新しく生成し保存
            GameObject itemObject = Instantiate(m_itemDatas.m_itemDatas[m_index].m_dropItemPrefabs, gameObject.transform.position, Quaternion.identity);
            m_retentionObject = itemObject;

            if (itemObject.TryGetComponent<Rigidbody>(out var itemObjectRb))
            {
                itemObjectRb.useGravity = false;
            }

            m_index++;
            m_inputTime = Time.time;

            if (m_index >= m_itemDatas.m_itemDatas.Count)
            {
                m_index = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_isGet = true;
        }
    }
}
