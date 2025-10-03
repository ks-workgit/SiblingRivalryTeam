using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFalling : MonoBehaviour
{
	CharacterManeger m_characterManeger;

	const int m_playerMaxHelth = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			m_characterManeger = other.GetComponent<CharacterManeger>();

			m_characterManeger.Damage(m_playerMaxHelth);
		}
	}
}
