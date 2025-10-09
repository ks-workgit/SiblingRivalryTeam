using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManeger : MonoBehaviour
{
	const int MaxHelth = 100;
	const int MaxShield = 100;
	static int[] RemainingLife = { 3, 5 };

	[SerializeField] int m_helth = MaxHelth;
	[SerializeField] int m_shield;
	[SerializeField] int m_crownCount = 0;
	[SerializeField] int m_remainingLife;

	bool m_isDeth = false;

	bool m_isRespawn = false;

	public int GetCrownCount()
	{
		return m_crownCount;
	}

	public bool GetIsDeth()
	{
		return m_isDeth;
	}

	public int GetHelth()
	{
		return m_helth;
	}

	public int GetShield()
	{
		return m_shield;
	}

	public int GetRemainingLife()
	{
		return m_remainingLife;
	}

	public void OnIsRespawn()
	{
		m_isRespawn = true;
	}

	// Start is called before the first frame update
	void Start()
    {
		m_remainingLife = RemainingLife[0];
	}

    // Update is called once per frame
    void Update()
    {
        if(m_helth <= 0)
		{
			KnockDown();
		}
		//HPやシールドがマックスになった時に
		if (m_helth >= MaxHelth)
		{
			m_helth = MaxHelth;
		}
		if(m_shield >= MaxShield)
		{
			m_shield = MaxShield;
		}

		if(m_remainingLife <= 0)
		{
			m_isDeth = true;
		}
    }

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
		if(m_shield > 0)
		{
			int beforeShield = m_shield;

			m_shield -= damage;

			damage -= beforeShield;

			if(damage > 0)
			{
				m_helth -= damage;
			}
		}
		else
		{
			m_helth -= damage;
		}

		Debug.Log("残りHP" +  m_helth);
	}

	public void Heal(int healValue)
	{
		m_helth += healValue;
	}

	public void GetShield(int shieldValue)
	{
		m_shield += shieldValue;
	}

	private void OnTriggerEnter(Collider other)
	{
		//王冠に触れたときに王冠の数を増やす
		if (other.CompareTag("Crown"))
		{
			m_crownCount++;

			Debug.Log("王冠げっと");
			Debug.Log("王冠かず" + m_crownCount);

			Destroy(other.gameObject);
		}
	}
}
