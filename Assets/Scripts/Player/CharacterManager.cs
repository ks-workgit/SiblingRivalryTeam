using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterManager : MonoBehaviour
{
	[SerializeField] int m_playerId;

	const int MaxHelth = 100;
	const float MaxStamina = 100;
	const int MaxShield = 100;
	static int[] RemainingLife = { 3, 5 };		//残機の初期数
	const int AttackDamage = 5;
	const int AttackSpeed = 10;

	[SerializeField] CharacterDatas m_characterDatas;
	[SerializeField] PlayerRespawn m_playerRespawn;

	[SerializeField] Slider m_healthBar;

	[SerializeField] int m_helth = MaxHelth;
	[SerializeField] float m_stamina = MaxStamina;
	[SerializeField] int m_shield;
	int m_remainingLife;

	int m_attackDamage = AttackDamage;
	int m_attackSpeed = AttackSpeed;

	bool m_isDeth = false;

	bool m_isRespawn = false;

	public void SetPlayerId(int playerId)
	{
		m_playerId = playerId;
	}

	public int GetPlayerId()
	{
		return m_playerId;
	}

	public int GetHelth()
	{
		return m_helth;
	}

	public float GetStamina()
	{
		return m_stamina;
	}

	public float GetMaxStamina()
	{
		return MaxStamina;
	}

	public int GetShield()
	{
		return m_shield;
	}

	public int GetRemainingLife()
	{
		return m_remainingLife;
	}

	public bool GetIsDeth()
	{
		return m_isDeth;
	}
	
	public void OnIsRespawn()
	{
		m_isRespawn = true;
	}

	public int GetSetAtttackDamage
	{
		get { return m_attackDamage; }
		set { m_attackDamage = value; }
	}

	public int GetSetAtttackSpeed
	{
		get { return m_attackSpeed; }
		set { m_attackSpeed = value; }
	}

	void Start()
    {
		m_remainingLife = RemainingLife[0];
	}

    void Update()
    {
        if(m_helth <= 0)
		{
			KnockDown();
		}

		if(m_remainingLife <= 0)
		{
			m_isDeth = true;

			m_characterDatas.IsDeth[m_playerId] = true;
		}
    }

	//体力がゼロになった時の処理
	void KnockDown()
	{
		if (m_isRespawn)
		{
			m_remainingLife--;

			m_helth = MaxHelth;

			m_isRespawn = false;
		}
	}

	//ダメージ処理
	public void Damage(int damage)
	{
		m_helth -= damage;
	}

	public void Heal(int healValue)
	{
		m_helth += healValue;
	}

    public void GetShield(int shieldValue)
	{
		m_shield += shieldValue;
	}
}
