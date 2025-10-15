using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloryInDeath : MonoBehaviour
{
	CharacterManager m_characterManeger;

	GameObject m_player;

    // Start is called before the first frame update
    void Start()
    {
		m_player = transform.parent.gameObject;

        m_characterManeger = m_player.GetComponent<CharacterManager>();
	}

    // Update is called once per frame
    void Update()
    {
        if(m_characterManeger.GetHelth() <= 0)
		{
			m_characterManeger.Heal(100);

			Debug.Log("–¼—_‚ ‚éŽ€”­“®");
		}
    }
}
