using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WitherBlade : MonoBehaviour
{
	const int InitialAttackDamage = 10;
	const float Duration = 5f;		//デバフの持続時間

	[SerializeField] GameObject m_debuffEffectObject;
	[SerializeField] WeaponDatas m_weaponDatas;

	CharacterManager m_characterManager;
	PlayerController m_playerController;
	TakeWeapon m_takeWeapon;

	Weapon m_weapon;

	GameObject m_debuffEffect;

	bool m_isDebuff;

    // Start is called before the first frame update
    void Start()
    {
        m_weapon = GetComponent<Weapon>();
    }

	// Update is called once per frame
	void Update()
	{
		if (m_weapon.GetHit() && !m_isDebuff)
		{
			m_playerController = m_weapon.GetEnemy().GetComponent<PlayerController>();

			if (!m_playerController.GetIsInvincible())
			{
				Debuff();
			}
		}

		if (m_isDebuff)
		{
			if (m_weapon.GetEnemy() != null)
			{
				if (m_takeWeapon.GetTakeDrop())
				{
					//攻撃力、攻撃スピードをリセット
					m_characterManager.GetSetAtttackDamage =
						InitialAttackDamage + m_weaponDatas.m_weaponDatas[m_takeWeapon.GetHaveWeaponId()].m_attackDamage;
					m_characterManager.GetSetAtttackSpeed =
						m_weaponDatas.m_weaponDatas[m_takeWeapon.GetHaveWeaponId()].m_attackSpeed;

					//攻撃力、攻撃スピードを下げてる
					m_characterManager.GetSetAtttackDamage /= 2;
					m_characterManager.GetSetAtttackSpeed /= 2;
				}

				if(m_characterManager.GetHelth() <= 0)
				{
					//攻撃力、攻撃スピードをリセット
					m_characterManager.GetSetAtttackDamage =
						InitialAttackDamage + m_weaponDatas.m_weaponDatas[m_takeWeapon.GetHaveWeaponId()].m_attackDamage;
					m_characterManager.GetSetAtttackSpeed =
						m_weaponDatas.m_weaponDatas[m_takeWeapon.GetHaveWeaponId()].m_attackSpeed;
					//スピードのリセット
					m_playerController.GetSetSpeedMagnification = 1;
					m_playerController.ChangeSpeed();

					m_isDebuff = false;

					Destroy(m_debuffEffect);
				}
			}
		}
	}

	void Debuff()
	{
		GameObject enemy = m_weapon.GetEnemy().gameObject;

		m_characterManager = enemy.GetComponent<CharacterManager>();
		
		m_takeWeapon = m_weapon.GetEnemy().GetComponent<TakeWeapon>();

		//攻撃力、攻撃スピードを下げてる
		m_characterManager.GetSetAtttackDamage /= 2;
		m_characterManager.GetSetAtttackSpeed /= 2;

		//移動速度を下げている
		m_playerController.GetSetSpeedMagnification -= 0.5f;
		m_playerController.ChangeSpeed();

		m_debuffEffect = Instantiate(
			m_debuffEffectObject,
			enemy.transform.position,
			Quaternion.identity,
			enemy.transform);

		StartCoroutine(ResetStatus());

		m_isDebuff = true;
	}

	IEnumerator ResetStatus()
	{
		yield return new WaitForSeconds(Duration);

		//攻撃力、攻撃スピードをリセット
		m_characterManager.GetSetAtttackDamage =
			InitialAttackDamage + m_weaponDatas.m_weaponDatas[m_takeWeapon.GetHaveWeaponId()].m_attackDamage;
		m_characterManager.GetSetAtttackSpeed =
			m_weaponDatas.m_weaponDatas[m_takeWeapon.GetHaveWeaponId()].m_attackSpeed;
		//スピードのリセット
		m_playerController.GetSetSpeedMagnification += 0.5f;
		m_playerController.ChangeSpeed();

		m_isDebuff = false;

		Destroy(m_debuffEffect);
	}
}
