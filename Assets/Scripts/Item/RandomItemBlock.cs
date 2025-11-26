using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemBlock : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] m_sprites;
    [SerializeField] ItemDatas m_itemDatas;

    int m_spriteIndex;

    float m_inputTime;
    float m_delay = 0.3f;

    private void Update()
    {
        // 一定間隔でインデックスを進める
        if (Time.time - m_inputTime > m_delay)
        {
            // アイコンを反映
            foreach (var sprite in m_sprites)
            {
                sprite.sprite = m_itemDatas.m_itemDatas[m_spriteIndex].m_itemIcon;
            }

            m_spriteIndex++;
            m_inputTime = Time.time;
            
            if (m_spriteIndex >= m_itemDatas.m_itemDatas.Count)
            {
                m_spriteIndex = 0;
            }
        }
    }
}
