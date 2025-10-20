using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloryInDeath : MonoBehaviour
{
	CharacterManager m_characterManeger;

	GameObject m_player;

	bool m_isUse;

	float m_damageCoolDown;

    // Start is called before the first frame update
    void Start()
    {
		m_player = transform.parent.gameObject;

        m_characterManeger = m_player.GetComponent<CharacterManager>();
	}

    // Update is called once per frame
    void Update()
    {
		if (m_isUse)
		{
			m_damageCoolDown -= Time.deltaTime;

			if (m_damageCoolDown < 0)
			{
				m_characterManeger.Damage(5);
			}
		}

		if (m_characterManeger.GetHelth() <= 0 && !m_isUse)
		{
			m_characterManeger.Heal(100);

			Debug.Log("–¼—_‚ ‚éŽ€”­“®");

			m_characterManeger.GetSetAtttackDamage = m_characterManeger.GetSetAtttackDamage * 5;
			m_characterManeger.GetSetAtttackSpeed = m_characterManeger.GetSetAtttackSpeed * 3;

			m_isUse = true;
		}
		else if(m_characterManeger.GetHelth() <= 0 && m_isUse)
		{
			m_characterManeger.GetSetAtttackDamage = m_characterManeger.GetSetAtttackDamage / 5;
			m_characterManeger.GetSetAtttackSpeed = m_characterManeger.GetSetAtttackSpeed / 3;

			Destroy(gameObject);
		}		
    }
}
