using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainSword : MonoBehaviour
{
	Weapon m_weapon;
	CharacterManager m_owner;
	PlayerController m_playerController;

    // Start is called before the first frame update
    void Start()
    {
		m_weapon = GetComponent<Weapon>();
	}

    // Update is called once per frame
    void Update()
    {
        if(m_weapon.GetHit())
		{
			m_playerController = m_weapon.GetEnemy().GetComponent<PlayerController>();

			if (!m_playerController.GetIsInvincible())
			{
				Drain();
			}
		}
    }

	void Drain()
	{
		m_owner = m_weapon.GetOwner().gameObject.GetComponent<CharacterManager>();

		m_owner.Heal(m_owner.GetSetAtttackDamage);
	}
}
