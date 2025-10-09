using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManeger : MonoBehaviour
{
	const int MaxHelth = 100;
	static int[] RemainingLife = { 3, 5 };		//残機の初期数
	const int AttackDamage = 5;
	const int AttackSpeed = 10;

	[SerializeField] int m_helth = MaxHelth;
	[SerializeField] int m_crownCount = 0;		//王冠持ってる数
	int m_remainingLife;

	int m_attackDamage = AttackDamage;
	int m_attackSpeed = AttackSpeed;

	bool m_isDeth = false;

	bool m_isRespawn = false;

	public void GetCrown()
	{
		m_crownCount++;
	}

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

	public int GetRemainingLife()
	{
		return m_remainingLife;
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

	//private void OnTriggerEnter(Collider other)
	//{
	//	//王冠に触れたときに王冠の数を増やす
	//	if (other.CompareTag("Crown"))
	//	{
	//		m_crownCount++;

	//		Debug.Log("王冠げっと");
	//		Debug.Log("王冠かず" + m_crownCount);

	//		Destroy(other.gameObject);
	//	}
	//}
}
