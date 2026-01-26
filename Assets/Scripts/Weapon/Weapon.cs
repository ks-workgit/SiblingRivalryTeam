using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] bool m_notStun;

	bool m_hit;
	Collider m_enemyCollider;

	GameObject m_owner;	

	public void SetOwner(GameObject owner)
	{
		m_owner = owner;
	}

	public GameObject GetOwner()
	{
		return m_owner;
	}

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

	public bool GetNotStun()
	{
		return m_notStun;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if(m_owner == other.gameObject) return;

			m_enemyCollider = other;

			m_hit = true;
		}
	}
}
