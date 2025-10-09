using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Shield : MonoBehaviour
{
	[SerializeField] int m_shildValue;
	CharacterManeger m_characterManeger;

	public void SetCharacterManeger(CharacterManeger characterManeger)
	{
		m_characterManeger = characterManeger;
	}
	public void GetShiled()
	{
		m_characterManeger.GetShield(m_shildValue);

		Debug.Log("シールド付与" + m_shildValue);

		Destroy(gameObject);
	}
}
