using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrike: MonoBehaviour
{
	[SerializeField] GameObject m_thunder;

	float m_thunderboltTime = 0.5f;  //—Ž—‹‚·‚é‚Ü‚Å‚ÌŽžŠÔ

	bool m_searchPlayer;

	Vector3 m_enemyPos;

	GameObject m_owner;

	private void Start()
	{
		StartCoroutine(UseSkill());

		m_owner = transform.parent.gameObject;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player") && !m_searchPlayer)
		{
			if (m_owner == other.gameObject) return;

			m_enemyPos = other.transform.position;

			m_enemyPos.y -= 1; 

			m_searchPlayer = true;
		}
	}

	IEnumerator UseSkill()
	{
		yield return new WaitForSeconds(m_thunderboltTime);

		Instantiate(m_thunder,m_enemyPos,Quaternion.identity);

		Destroy(gameObject);
	}
}
