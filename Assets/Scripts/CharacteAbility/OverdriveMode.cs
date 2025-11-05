using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverdriveMode : MonoBehaviour
{
	CharacterManager m_characterManeger;
	PlayerController m_playerController;

	const int SkillDuration = 10;	//スキルの長さ
	const int Magnification = 2;	//強化の倍率

	GameObject m_player;

	private void Start()
	{
		m_player = transform.parent.gameObject;

		m_characterManeger = m_player.GetComponent<CharacterManager>();
		m_playerController = m_player.GetComponent<PlayerController>();

		StartCoroutine(UseSkill());
	}

	IEnumerator UseSkill()
	{
		int attackDamage = m_characterManeger.GetSetAtttackDamage;
		float attackSpeed = m_characterManeger.GetSetAtttackSpeed;
		float speedMagnification = m_playerController.GetSetSpeedMagnification;

		attackDamage *= Magnification;
		attackSpeed *= Magnification;
		speedMagnification *= Magnification;

		m_characterManeger.GetSetAtttackDamage = attackDamage;
		m_characterManeger.GetSetAtttackSpeed = attackSpeed;

		m_playerController.GetSetSpeedMagnification = speedMagnification;

		m_playerController.ChangeSpeed();

		Debug.Log("オーバードライブモード使用");

		yield return new WaitForSeconds(SkillDuration);

		attackDamage /= Magnification;
		attackSpeed /= Magnification;
		speedMagnification /= Magnification;

		m_characterManeger.GetSetAtttackDamage = attackDamage;
		m_characterManeger.GetSetAtttackSpeed = attackSpeed;

		m_playerController.GetSetSpeedMagnification = speedMagnification;

		m_playerController.InitializationSpeed();

		Debug.Log("オーバードライブモード終了");

		Destroy(gameObject);
	}
}
