using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.GridLayoutGroup;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject m_owner;
	[SerializeField] CharacterManager m_characterManager;
    int m_damage;

    Collider m_collider;

	CharacterManager m_enemy;

    private void Start()
    {
        m_collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ©•ª©g‚È‚ç–³‹
        if (other.transform.root == m_owner.transform) return;

        if (other.CompareTag("Player"))
        {
			m_damage = m_characterManager.GetSetAtttackDamage;

			Debug.Log($"{m_owner.name} hit {other.name}");
            
			m_enemy = other.GetComponent<CharacterManager>();
			m_enemy.Damage(m_damage);
            Debug.Log("ƒqƒbƒg");

            // ‘½’iƒqƒbƒg–h~
            m_collider.enabled = false;
        }
    }
}
