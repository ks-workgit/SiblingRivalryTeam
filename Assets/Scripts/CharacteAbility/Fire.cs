using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
	const float DamageColldwon = 1;

	const int Damage = 10;

	float m_damageColldwon;

	CharacterManager m_characterManeger;
	GameObject m_player;

	public void SetPlayer(GameObject player)
	{
		m_player = player;
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			if (m_player == other.gameObject) return;

			m_damageColldwon -= Time.deltaTime;

			m_characterManeger = other.GetComponent<CharacterManager>();

			if (m_damageColldwon <= 0 )
			{
				m_characterManeger.ReduceHealth(Damage);

				m_damageColldwon = DamageColldwon;
			}
		}
	}
}
