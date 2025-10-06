using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverdriveMode : MonoBehaviour
{
	[SerializeField] CharacterManeger m_characterManeger;

	const int SkillDuration = 10;
	const int magnification = 2;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			StartCoroutine(UseSkill());
		}
	}

	IEnumerator UseSkill()
	{
		int attackDamage = m_characterManeger.GetSetAtttackDamage;
		int attackSpeed = m_characterManeger.GetSetAtttackSpeed;

		attackDamage *= magnification;
		attackSpeed *= magnification;

		m_characterManeger.GetSetAtttackDamage = attackDamage;
		m_characterManeger.GetSetAtttackSpeed = attackSpeed;

		Debug.Log("オーバードライブモード使用");

		yield return new WaitForSeconds(SkillDuration);

		attackDamage /= magnification;
		attackSpeed /= magnification;

		m_characterManeger.GetSetAtttackDamage = attackDamage;
		m_characterManeger.GetSetAtttackSpeed = attackSpeed;

		Debug.Log("オーバードライブモード終了");
	}
}
