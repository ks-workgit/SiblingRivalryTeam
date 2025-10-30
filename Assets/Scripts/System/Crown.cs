using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour
{
	[SerializeField] CharacterDatas m_characterDatas;
	CharacterManager m_characterManeger;

	BoxCollider m_boxCollider;

	AudioSource	m_se;
	MeshRenderer m_mesh;

	bool m_isGeted;

	private void Start()
	{
		m_se = GetComponent<AudioSource>();
		m_mesh = GetComponent<MeshRenderer>();
		m_boxCollider = GetComponent<BoxCollider>();
	}

	private void Update()
	{
		if(m_isGeted)
		{
			if (!m_se.isPlaying)
			{
				Destroy(gameObject);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			m_characterManeger = other.GetComponent<CharacterManager>();

			m_characterDatas.CrownCount[m_characterManeger.GetPlayerId()]++;

			m_se.Play();
			m_mesh.enabled = false;
			m_boxCollider.enabled = false;

			m_isGeted = true;
		}
	}
}
