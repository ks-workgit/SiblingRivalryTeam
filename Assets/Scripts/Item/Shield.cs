using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Shield : MonoBehaviour
{
	[SerializeField] int m_shildValue;
	CharacterManager m_characterManeger;

    [SerializeField] AudioSource m_audioSource;

	bool m_isUse;

    private void Update()
    {
		if(m_isUse)
		{
			if(!m_audioSource.isPlaying)
			{
				Destroy(gameObject);
			}
		}
    }

    public void SetCharacterManeger(CharacterManager characterManeger)
	{
		m_characterManeger = characterManeger;
	}
	public void GetShiled()
	{
		m_characterManeger.GetShield(m_shildValue);
		m_audioSource.Play();

		Debug.Log("シールド付与" + m_shildValue);

		m_isUse = true;
    }
}
