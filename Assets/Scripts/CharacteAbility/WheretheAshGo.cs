using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheretheAshGo : MonoBehaviour
{
	[SerializeField] GameObject FirePrefab;
	const float FireDuration = 10;
	const float FireColldown = 0.5f;

	const int SkillDuration = 10;

	float m_fireColldown;
	bool m_isUseSkill;

	GameObject m_fire;

	private void Update()
	{
		m_fireColldown += Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.E))
		{
			StartCoroutine(UseSkill());
		}

		if(m_isUseSkill)
		{
			if (m_fireColldown >= FireColldown)
			{
				

				GameObject m_fire = Instantiate(
					FirePrefab,
					gameObject.transform.position,
					Quaternion.identity
					);

				m_fireColldown = 0;
			}
		}
	}

	IEnumerator UseSkill()
	{
		m_isUseSkill = true;

		Debug.Log("灰の行方使用");

		yield return new WaitForSeconds(SkillDuration);

		m_isUseSkill = false;

		Debug.Log("灰の行方終了");
	}


}
