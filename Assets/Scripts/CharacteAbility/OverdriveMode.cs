using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverdriveMode : MonoBehaviour
{
	CharacterManager m_characterManeger;
	PlayerController m_playerController;
	TakeWeapon m_takeWeapon;

	const int SkillDuration = 15;	//スキルの長さ
	const int Magnification = 2;	//強化の倍率

	GameObject m_player;

	int m_noBuffDamage;
	float m_noBuffSpeed;

	private void Start()
	{
		m_player = transform.parent.gameObject;

		m_characterManeger = m_player.GetComponent<CharacterManager>();
		m_playerController = m_player.GetComponent<PlayerController>();
		m_takeWeapon = m_player.GetComponent<TakeWeapon>();

		m_noBuffDamage = m_takeWeapon.GetNoBuffDamage();
		m_noBuffSpeed = m_takeWeapon.GetNoBuffSpeed();

		StartCoroutine(UseSkill());
	}

	private void Update()
	{
		if (m_takeWeapon.GetTakeDrop())
		{
			m_noBuffDamage = m_takeWeapon.GetNoBuffDamage();
			m_noBuffSpeed = m_takeWeapon.GetNoBuffSpeed();

			m_noBuffDamage /= Magnification;
			m_noBuffSpeed /= Magnification;

			m_characterManeger.GetSetAtttackDamage = m_noBuffDamage * Magnification;
			m_characterManeger.GetSetAtttackSpeed = m_noBuffSpeed * Magnification;

			m_takeWeapon.ResetTakeDrop();
		}
	}

	IEnumerator UseSkill()
	{
		float speedMagnification = m_playerController.GetSetSpeedMagnification;
		speedMagnification *= Magnification;

		m_characterManeger.GetSetAtttackDamage = m_noBuffDamage * Magnification;
		m_characterManeger.GetSetAtttackSpeed = m_noBuffSpeed * Magnification;

		m_playerController.GetSetSpeedMagnification = speedMagnification;

		m_playerController.ChangeSpeed();

		Debug.Log("オーバードライブモード使用");

		yield return new WaitForSeconds(SkillDuration);

		speedMagnification /= Magnification;

		m_characterManeger.GetSetAtttackDamage = m_takeWeapon.GetNoBuffDamage();
		m_characterManeger.GetSetAtttackSpeed = m_takeWeapon.GetNoBuffSpeed();

		m_playerController.GetSetSpeedMagnification = speedMagnification;

		m_playerController.InitializationSpeed();

		Debug.Log("オーバードライブモード終了");

		Destroy(gameObject);
	}
}
