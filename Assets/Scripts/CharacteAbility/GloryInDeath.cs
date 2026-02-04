using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloryInDeath : MonoBehaviour
{
	[SerializeField] GameObject m_effect;
	[SerializeField] WeaponDatas m_weaponDatas;
	[SerializeField] AudioSource m_se;

	const int NoWeaponDamage = 10;
	const float DamageCoolDown = 5;
	const int Damage = 20;

	CharacterManager m_characterManeger;
	PlayerController m_playerController;
	TakeWeapon m_takeWeapon;

	GameObject m_player;

	bool m_isUse;

	int m_noBuffDamage;
	float m_noBuffSpeed;
	float m_damageCoolDown;

    // Start is called before the first frame update
    void Start()
    {
		m_player = transform.parent.gameObject;

        m_characterManeger = m_player.GetComponent<CharacterManager>();
		m_playerController = m_player.GetComponent<PlayerController>();
		m_takeWeapon = m_player.GetComponent<TakeWeapon>();
	}

    // Update is called once per frame
    void Update()
    {
		if (m_isUse)
		{
			m_damageCoolDown -= Time.deltaTime;

			m_effect.SetActive(true);

			if (m_damageCoolDown < 0)
			{
				m_characterManeger.ReduceHealth(Damage);

				m_damageCoolDown = DamageCoolDown;
			}

			if (m_takeWeapon.GetTakeDrop())
			{
				m_noBuffDamage = NoWeaponDamage + m_weaponDatas.m_weaponDatas[m_takeWeapon.GetHaveWeaponId()].m_attackDamage;
				m_noBuffSpeed = m_weaponDatas.m_weaponDatas[m_takeWeapon.GetHaveWeaponId()].m_attackSpeed;

				m_characterManeger.GetSetAtttackDamage = m_noBuffDamage * 5;
				m_characterManeger.GetSetAtttackSpeed = m_noBuffSpeed * 3;

				m_takeWeapon.ResetTakeDrop();
			}
		}	

		if (m_characterManeger.GetHelth() <= 0 && !m_isUse)
		{
			m_noBuffDamage = NoWeaponDamage + m_weaponDatas.m_weaponDatas[m_takeWeapon.GetHaveWeaponId()].m_attackDamage;
			m_noBuffSpeed = m_weaponDatas.m_weaponDatas[m_takeWeapon.GetHaveWeaponId()].m_attackSpeed;

			m_se.Play();
			m_characterManeger.Heal(100);

			Debug.Log("–¼—_‚ ‚éŽ€”­“®");

			m_characterManeger.GetSetAtttackDamage = m_noBuffDamage * 5;
			m_characterManeger.GetSetAtttackSpeed = m_noBuffSpeed * 3;

			m_playerController.GetSetSpeedMagnification += 0.5f;

			m_playerController.ChangeSpeed();

			m_isUse = true;
		}
		else if(m_characterManeger.GetHelth() <= 0 && m_isUse)
		{
			m_characterManeger.GetSetAtttackDamage = NoWeaponDamage + m_weaponDatas.m_weaponDatas[m_takeWeapon.GetHaveWeaponId()].m_attackDamage;
			m_characterManeger.GetSetAtttackSpeed = m_weaponDatas.m_weaponDatas[m_takeWeapon.GetHaveWeaponId()].m_attackSpeed;

			m_playerController.GetSetSpeedMagnification = 1;

			m_playerController.ChangeSpeed();
		}

		if (m_characterManeger.GetIsRespawn())
		{
			m_effect.SetActive(false);
			m_isUse = false;
		}
	}
}
