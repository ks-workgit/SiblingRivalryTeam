using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Rule Setting", fileName = "RuleSettingsData")]
public class RuleSettingsData : ScriptableObject
{
    public int[] RoundsCount = { 1, 2, 3 }; // ラウンド数

    public int[] LifeCount = { 1, 2, 3 };   // 残機の数

    public int[] VictoryCrownCount = { 3, 5, 10 };  // 勝利するための王冠の数

    // 配列の何番目かを決める用
    public int m_roundCount;
    public int m_lifeCount;
    public int m_crownCount;
}
