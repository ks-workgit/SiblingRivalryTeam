using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
	const int DamageValue = 50;

	[SerializeField] GameObject bombEffect;

	BoxCollider m_boxCollider;

	private void Start()
	{
		m_boxCollider = GetComponent<BoxCollider>();

		StartCoroutine(OnCollider());
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();

			rigidbody.isKinematic = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			CharacterManager characterManager = other.GetComponent<CharacterManager>();

			characterManager.Damage(DamageValue);

			Instantiate(bombEffect, transform.position, Quaternion.identity);

			Destroy(gameObject);
		}
	}

	IEnumerator OnCollider()
	{
		yield return new WaitForSeconds(0.5f);

		m_boxCollider.enabled = true;
	}
}
