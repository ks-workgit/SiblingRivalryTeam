using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWeapon : MonoBehaviour
{
	[SerializeField] WeaponDatas m_weaponDatas;

	[SerializeField] Transform[] WeaponSpownPos;

	private void Start()
	{
		SpownCrown();
	}

	void SpownCrown()
	{
		int spownIndex = Random.Range(0, WeaponSpownPos.Length);
		int weaponIndex = Random.Range(0, m_weaponDatas.m_weaponDatas.Count);

		Instantiate(
			m_weaponDatas.m_weaponDatas[weaponIndex].m_dropWeaponPrefabs,
			WeaponSpownPos[spownIndex].position,
			Quaternion.Euler(-90, 0, 0)
			);
	}
}
