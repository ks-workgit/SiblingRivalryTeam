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
		if (other.CompareTag("Player"))
		{
			if (other.transform.position == m_player.transform.position) return;

			CharacterController enemy = other.GetComponent<CharacterController>();
			CharacterController player = m_player.GetComponent<CharacterController>();

			enemy.enabled = false;
			player.enabled = false;

			Vector3 enemyPosition = other.transform.position;
			Vector3 playerPosition = m_player.transform.position;

			Debug.Log("自身" + playerPosition + "相手" + enemyPosition);

			m_player.transform.position = enemyPosition;
			other.transform.position = playerPosition;

			Debug.Log("ファントムトレード発動");
			Debug.Log("自身" + m_player.transform.position + "相手" + other.transform.position);

			enemy.enabled = true;
			player.enabled = true;

			Destroy(gameObject);
		}
	}
}
