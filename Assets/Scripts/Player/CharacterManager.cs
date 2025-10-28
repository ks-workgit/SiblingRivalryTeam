using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterManager : MonoBehaviour
{
	[SerializeField] int m_playerId;

	const float MaxHelth = 100;
	const float MaxStamina = 30;
	const int MaxShield = 100;
	static int[] RemainingLife = { 3, 5 };		//残機の初期数
	const int AttackDamage = 10;
	const int AttackSpeed = 1;

	[SerializeField] CharacterDatas m_characterDatas;
	[SerializeField] PlayerRespawn m_playerRespawn;
	[SerializeField] PlayerController m_playerController;

	[SerializeField] Slider m_healthBar;

	[SerializeField] float m_helth = MaxHelth;
	[SerializeField] float m_stamina = MaxStamina;
	[SerializeField] int m_shield;
	int m_remainingLife;

	[SerializeField] int m_attackDamage = AttackDamage;
	[SerializeField] int m_attackSpeed = AttackSpeed;

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

	public float GetHelth()
	{
		return m_helth;
	}

    public float GetMaxHealth()
    {
        return MaxHelth;
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

	public void ReduceHealth(float reduceValue)
	{
		m_helth -= reduceValue;
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
	public void KnockDown()
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
		// 無敵じゃないとき
		if (!m_playerController.GetIsInvincible())
		{
			m_playerController.SetIsStun(true);
			m_helth -= damage;
		}
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
