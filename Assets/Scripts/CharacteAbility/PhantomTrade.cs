using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomTrade : MonoBehaviour
{
	GameObject m_player;

	private void Start()
	{
		m_player = transform.parent.gameObject;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			Vector3 enemyPosition = other.transform.position;
			Vector3 playerPosition = m_player.transform.position;

			m_player.transform.position = enemyPosition;

			other.transform.position = playerPosition;

			Debug.Log("ファントムトレード発動");

			Destroy(gameObject);
		}
	}
}
