using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWeapon : MonoBehaviour
{
	[SerializeField] int m_WeaponId;

	public int GetWeaponId()
	{
		return m_WeaponId;
	}
}
