using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class FateDice : MonoBehaviour
{
	Weapon m_weapon;

	int[] Dice = { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 };

	CharacterManager m_owner;
	int m_ownerAttackDamage;
	PlayerController m_playerController;

	// Start is called before the first frame update
	void Start()
    {
		m_weapon = GetComponent<Weapon>();
		m_owner = m_weapon.GetOwner().GetComponent<CharacterManager>();
	}

    // Update is called once per frame
    void Update()
    {
		if (m_weapon.GetHit())
		{
			m_playerController = m_weapon.GetEnemy().GetComponent<PlayerController>();

			if (!m_playerController.GetIsGrounded())
			{
				Lottery();
			}
		}
		else
		{
			m_owner.GetSetAtttackDamage = m_ownerAttackDamage;
		}
	}

	//’Š‘I30%‚ÌŠm—¦‚ÅˆêŒ‚‚ÅŽ€–S
	void Lottery()
	{
		int diceRoll = Random.Range(0, Dice.Length);

		if (Dice[diceRoll] == 0)
		{
			CharacterManager enemy = m_weapon.GetComponent<CharacterManager>();

			enemy.HelthToZero();

			Dice = new int[]{ 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 };
		}
		else
		{
			Dice[diceRoll] = 0;
		}
	}
}
