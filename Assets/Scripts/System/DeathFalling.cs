using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFalling : MonoBehaviour
{
	CharacterManager m_characterManeger;

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			m_characterManeger = other.GetComponent<CharacterManager>();

			m_characterManeger.ReduceHealth(m_characterManeger.GetMaxHealth());
		}
	}
}
