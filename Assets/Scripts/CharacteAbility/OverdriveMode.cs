using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverdriveMode : MonoBehaviour
{
	CharacterManager m_characterManeger;

	const int SkillDuration = 10;	//スキルの長さ
	const int Magnification = 2;	//強化の倍率

	GameObject m_player;

	private void Start()
	{
		m_player = transform.parent.gameObject;

		m_characterManeger = m_player.GetComponent<CharacterManager>();

		StartCoroutine(UseSkill());
	}

	IEnumerator UseSkill()
	{
		int attackDamage = m_characterManeger.GetSetAtttackDamage;
		int attackSpeed = m_characterManeger.GetSetAtttackSpeed;

		attackDamage *= Magnification;
		attackSpeed *= Magnification;

		m_characterManeger.GetSetAtttackDamage = attackDamage;
		m_characterManeger.GetSetAtttackSpeed = attackSpeed;

		Debug.Log("オーバードライブモード使用");

		yield return new WaitForSeconds(SkillDuration);

		attackDamage /= Magnification;
		attackSpeed /= Magnification;

		m_characterManeger.GetSetAtttackDamage = attackDamage;
		m_characterManeger.GetSetAtttackSpeed = attackSpeed;

		Debug.Log("オーバードライブモード終了");

		Destroy(gameObject);
	}
}
