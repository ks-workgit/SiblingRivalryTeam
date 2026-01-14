using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.Progress;

public class RandomItemBox : MonoBehaviour
{
    [SerializeField] ItemDatas m_itemDatas;
    DropItem m_dropItem;

    GameObject m_retentionObject;   // 前のオブジェクト保存用

    int m_index;

    float m_inputTime;
    float m_delay = 0.3f;

    bool m_isGet = false;
    bool m_isFirst = false;

    private void Update()
    {
        if(!m_isGet)
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

                m_dropItem = itemObject.GetComponent<DropItem>();

                m_index++;
                m_inputTime = Time.time;

                if (m_index >= m_itemDatas.m_itemDatas.Count)
                {
                    m_index = 0;
                }

                m_isFirst = true;
            }            
        }

        if (m_isFirst) m_isGet = m_dropItem.GetIsTouch();
    }
}
