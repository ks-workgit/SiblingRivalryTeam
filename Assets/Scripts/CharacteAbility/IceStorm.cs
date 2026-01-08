using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceStorm : MonoBehaviour
{
	const int Damage = 10;

	GameObject m_usePlayer;

	float m_damageCooldown;

	public void SetUsePlayer(GameObject usePlayer)
	{
		m_usePlayer = usePlayer;
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			m_damageCooldown += Time.deltaTime;

			if (other.gameObject == m_usePlayer) return;

			CharacterManager characterManager = other.GetComponent<CharacterManager>();

			//ƒ_ƒ[ƒW
			if(m_damageCooldown >= 1)
			{
				characterManager.ReduceHealth(Damage);

				m_damageCooldown = 0;
			}
		}
	}
}
