using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponDatas")]

public class WeaponDatas : ScriptableObject
{
	public List<WeaponInfomertions> m_weaponDatas;
}

[System.Serializable]
public class WeaponInfomertions
{
	public GameObject m_weaponPrefabs;
	public GameObject m_dropWeaponPrefabs;
	public int m_weaponId;
	public int m_weaponKindId;
	public int m_attackDamage;
	public float m_attackSpeed = 1.0f;
	public int m_tier;
}