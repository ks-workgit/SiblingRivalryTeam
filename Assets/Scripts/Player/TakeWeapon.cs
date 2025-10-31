using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeWeapon : MonoBehaviour
{
	[SerializeField] Transform m_handPos;

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Weapon"))
		{

		}
	}
}
