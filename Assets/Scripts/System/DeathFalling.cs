using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFalling : MonoBehaviour
{
	CharacterManager m_characterManeger;

	const int MaxHealtValue = 200;

	private void OnTriggerStay(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			m_characterManeger = other.GetComponent<CharacterManager>();

			m_characterManeger.ReduceHealth(MaxHealtValue);
		}

		if(other.CompareTag("Item") || other.CompareTag("Weapon") || other.CompareTag("Crown"))
		{
			Destroy(other.gameObject);
		}
	}
}
