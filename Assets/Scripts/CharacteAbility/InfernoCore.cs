using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernoCore : MonoBehaviour
{
	const int DamageValue = 75;

	GameObject m_player;

	private void Start()
	{
		m_player = transform.parent.gameObject;

		gameObject.transform.parent = null;

		StartCoroutine(OffCollider());
	}

	private void Update()
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			if (other.gameObject == m_player) return;

			CharacterManager characterManager = other.GetComponent<CharacterManager>();

			characterManager.Damage(DamageValue);

			Collider collider = gameObject.GetComponent<Collider>();

			collider.enabled = false;
		}
	}

	IEnumerator OffCollider()
	{
		Collider collider = gameObject.GetComponent<Collider>();

		yield return new WaitForSeconds(0.5f);		

		collider.enabled = false;
	}
}
