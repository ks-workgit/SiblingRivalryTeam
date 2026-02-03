using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.GridLayoutGroup;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject m_owner;
	[SerializeField] GameObject m_hand;
	[SerializeField] CharacterManager m_characterManager;
	[SerializeField] PlayerController m_playerController;
	[SerializeField] TakeWeapon m_takeWeapon;

	GameObject m_weaponObject;
	Weapon m_weapon;

    int m_damage;

    Collider m_collider;

	CharacterManager m_enemy;
	AudioSource m_se;	//‰£‚éSe

    private void Start()
    {
        m_collider = GetComponent<Collider>();
		m_playerController.SetCollider(m_collider);
		m_se = GetComponent<AudioSource>();

		m_collider.enabled = false;
	}

	private void Update()
	{
		if(m_hand.transform.childCount > 0)
		{
			m_weaponObject = m_hand.transform.GetChild(0).gameObject;

			m_collider = m_weaponObject.GetComponent<Collider>();
			m_weapon = m_weaponObject.GetComponent<Weapon>();
			//•Ší‚ğ‚Á‚Ä‚¢‚é‚È‚ç•Ší‚ÌƒRƒ‰ƒCƒ_[‚ğ“n‚·
			m_playerController.SetCollider(m_collider);

			if(m_playerController.GetAttackStart())
			{
				AudioSource se = m_weaponObject.GetComponent<AudioSource>();
				Debug.Log("”ö‚Æ‚È‚Á‚½");

				se.Play();
				m_playerController.ResetAttackStart();

			}

			//•Ší‚ğU‚Á‚Ä“G‚É“–‚½‚Á‚½‚ç’Ê‚é
			if (m_weapon.GetHit())
			{
				if(m_weapon.GetNotStun())
				{
					NotStunAttackHit(m_weapon.GetEnemy());
				}
				else
				{
					AttackHit(m_weapon.GetEnemy());
				}

				m_weapon.ResetHit();
			}
		}
		else
		{
			m_collider = GetComponent<Collider>();

			//•Ší‚ğ‚Á‚Ä‚¢‚È‚¢‚È‚çŒ³‚Ì” ‚ÌƒRƒ‰ƒCƒ_[‚ğ“n‚·
			m_playerController.SetCollider(m_collider);

			if(m_playerController.GetAttackStart())
			{
				Debug.Log("”ö‚Æ‚È‚Á‚½");

				//‰£‚éSeÄ¶
				m_se.Play();

				m_playerController.ResetAttackStart();
			}
		}		
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
			AttackHit(other);          
        }
    }

	//UŒ‚‚ª“–‚½‚Á‚½‚Ìˆ—
	void AttackHit(Collider enemy)
	{
		// ©•ª©g‚È‚ç–³‹
		if (enemy.transform.root == m_owner.transform) return;

		m_damage = m_characterManager.GetSetAtttackDamage;

		Debug.Log($"{m_owner.name} hit {enemy.name}");

		m_enemy = enemy.GetComponent<CharacterManager>();
		m_enemy.Damage(m_damage);
		Debug.Log("ƒqƒbƒg");

		// ‘½’iƒqƒbƒg–h~
		m_collider.enabled = false;
	}

	//ƒXƒ^ƒ“‚µ‚È‚¢•Ší‚Ìê‡‚ÌUŒ‚
	void NotStunAttackHit(Collider enemy)
	{
		// ©•ª©g‚È‚ç–³‹
		if (enemy.transform.root == m_owner.transform) return;

		m_damage = m_characterManager.GetSetAtttackDamage;

		Debug.Log($"{m_owner.name} hit {enemy.name}");

		m_enemy = enemy.GetComponent<CharacterManager>();
		m_enemy.ReduceHealth(m_damage);
		Debug.Log("ƒqƒbƒg");

		// ‘½’iƒqƒbƒg–h~
		m_collider.enabled = false;
	}
}
