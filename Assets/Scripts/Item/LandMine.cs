using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
	const int DamageValue = 50;

	[SerializeField] GameObject bombEffect;
	[SerializeField] AudioSource m_se;

	[SerializeField] SphereCollider m_sphereCollider;
	[SerializeField] BoxCollider m_boxCollider;

	bool m_isExplosion;
	bool m_installationl;   //地雷を設置できたか

	private void Update()
	{
		if(m_isExplosion && !m_se.isPlaying)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();

			rigidbody.isKinematic = true;

			m_installationl = true;
		}

		//離れた後周囲にいるプレイヤーにダメージ
		if (other.CompareTag("Player") && m_isExplosion)
		{
			CharacterManager characterManager = other.GetComponent<CharacterManager>();

			characterManager.Damage(DamageValue);

			m_sphereCollider.enabled = false;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		//プレイヤーが地雷を踏んで離れたら
		if(other.CompareTag("Player") && m_installationl)
		{
			Instantiate(bombEffect, transform.position, Quaternion.identity);

			m_isExplosion = true;
			m_se.Play();

			m_sphereCollider.enabled = true; 
			m_boxCollider.enabled = false;
		}
	}
}
