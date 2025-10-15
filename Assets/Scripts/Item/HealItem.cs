using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
	[SerializeField] int m_healValue;
	
	CharacterManager m_characterManeger;

	public void SetCharacterManeger(CharacterManager characterManeger)
	{
		m_characterManeger = characterManeger;
	}
	public void Heal()
	{
		m_characterManeger.Heal(m_healValue);

		Debug.Log("‰ñ•œ" + m_healValue);

		Destroy(gameObject);
	}
}
