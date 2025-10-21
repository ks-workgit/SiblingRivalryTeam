using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilityDatas")]
public class AbilityDatas : ScriptableObject
{
	public List<AbilityData> m_abilityInfometions;
}

[System.Serializable]
public class AbilityData
{
	public GameObject m_abilityPrefab;
	public Sprite m_abilityIcon;
	public float m_abilityCoolDown;
}