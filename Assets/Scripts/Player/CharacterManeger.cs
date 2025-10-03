using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManeger : MonoBehaviour
{
	const int MaxHelth = 100;
	static int[] RemainingLife = { 3, 5 };

	int m_helth = MaxHelth;
	int m_crownCount = 0;
	int m_remainingLife;

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

	//ƒ_ƒ[ƒWˆ—
	public void Damage(int damage)
	{
		m_helth -= damage;
	}	

	private void OnTriggerEnter(Collider other)
	{
		//‰¤Š¥‚ÉG‚ê‚½‚Æ‚«‚É‰¤Š¥‚Ì”‚ð‘‚â‚·
		if (other.CompareTag("Crown"))
		{
			m_crownCount++;

			Debug.Log("‰¤Š¥‚°‚Á‚Æ");
			Debug.Log("‰¤Š¥‚©‚¸" + m_crownCount);

			Destroy(other.gameObject);
		}
	}
}
