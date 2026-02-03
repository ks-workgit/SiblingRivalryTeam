using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
	[SerializeField] int m_healValue;
	
	CharacterManager m_characterManeger;

    [SerializeField]  AudioSource m_audioSource;

    bool m_isUse;

    private void Update()
    {
        if (m_isUse)
        {
            if (!m_audioSource.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetCharacterManeger(CharacterManager characterManeger)
	{
		m_characterManeger = characterManeger;
	}
	public void Heal()
	{
		m_audioSource.Play();
		m_characterManeger.Heal(m_healValue);

		Debug.Log("‰ñ•œ" + m_healValue);

        m_isUse = true;
    }
}
