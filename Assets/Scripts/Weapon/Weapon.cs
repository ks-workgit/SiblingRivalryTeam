using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	bool m_hit;
	Collider m_enemyCollider;

	public bool GetHit()
	{
		return m_hit;
	}

	public void ResetHit()
	{
		m_hit = false;
	}

	public Collider GetEnemy()
	{
		return m_enemyCollider;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			m_enemyCollider = other;

			m_hit = true;
		}
	}
}
