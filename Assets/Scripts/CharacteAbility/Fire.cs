using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
	const float DamageColldwon = 1;

	const int Damage = 5;

	float m_damageColldwon;

	CharacterManeger m_characterManeger;

	private void OnTriggerStay(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			m_damageColldwon += Time.deltaTime;

			m_characterManeger = other.GetComponent<CharacterManeger>();

			if (m_damageColldwon >= DamageColldwon )
			{
				m_characterManeger.Damage(Damage);

				m_damageColldwon = 0;
			}
		}
	}
}
