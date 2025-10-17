using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour
{
	[SerializeField] CharacterDatas m_characterDatas;
	CharacterManeger m_characterManeger;
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			m_characterManeger = other.GetComponent<CharacterManeger>();

			m_characterDatas.CrownCount[m_characterManeger.GetPlayerId()]++;

			Destroy(gameObject);
		}
	}
}
