using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advantageous : MonoBehaviour
{
	[SerializeField] CharacterDatas m_characterDatas;

	private List<CharacterManager> m_characterManager = new List<CharacterManager>();

	int m_ownerId;
	int m_enemyId;

	int m_difference;	//残機や王冠の数の差

	public enum Situation
	{
		Advantage,			//有利
		SlightAdvantage,	//微有利
		Even,               //互角
		SlightDisadvantage,	//微不利
		Disadvantage		//不利
	}

	Situation m_ownerSituation;

	public Situation GetSituation()
	{
		return m_ownerSituation;
	}

	public void SetCharacterManager(List<CharacterManager> characterManager)
	{
		m_characterManager.AddRange(characterManager);
	}

	private void Start()
	{
		CharacterManager characterManager = GetComponent<CharacterManager>();
		m_ownerId = characterManager.GetPlayerId();

		if(m_ownerId == 0)
		{
			m_enemyId = 1;
		}
		else
		{
			m_enemyId = 0;
		}
	}

	private void Update()
	{
		m_difference =
			(m_characterDatas.CrownCount[m_ownerId] - m_characterDatas.CrownCount[m_enemyId]) + 
			(m_characterManager[m_ownerId].GetRemainingLife() - m_characterManager[m_enemyId].GetRemainingLife());			

		if (m_difference > 3)
		{
			m_ownerSituation = Situation.Advantage;
		}
		else if(m_difference > 0)
		{
			m_ownerSituation = Situation.SlightAdvantage;
		}
		else if (m_difference == 0)
		{
			m_ownerSituation = Situation.Even;
		}
		else if (m_difference < -3)
		{
			m_ownerSituation = Situation.Disadvantage;
		}
		else if(m_difference < 0)
		{
			m_ownerSituation = Situation.SlightDisadvantage;
		}
	}
}
