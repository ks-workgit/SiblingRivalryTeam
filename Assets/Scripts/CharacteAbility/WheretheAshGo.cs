using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheretheAshGo : MonoBehaviour
{
	[SerializeField] GameObject FirePrefab;
	const float FireColldown = 0.5f;		//炎を出すクールダウン

	const int SkillDuration = 10;       //スキルの長さ

	float m_fireColldown;
	bool m_isUseSkill;

	private void Start()
	{
        StartCoroutine(UseSkill());
	}

	private void Update()
	{
		m_fireColldown += Time.deltaTime;

		if(m_isUseSkill)
		{
			if (m_fireColldown >= FireColldown)
			{
				Instantiate(
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

		Destroy(gameObject);
	}
}
