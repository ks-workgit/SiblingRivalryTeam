using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseAbility : MonoBehaviour
{
	[SerializeField] AbilityDatas m_abilityDatas;

	[SerializeField] int m_characterId;

	Image m_abilityIcon;

	float m_ablityCoolDown;

	public void SetAbilityIcon(Image abilityIcon)
	{
		m_abilityIcon = abilityIcon;
	}

	private void Start()
	{
		m_abilityIcon.sprite = m_abilityDatas.m_abilityInfometions[m_characterId].m_abilityIcon;
	}

	// Update is called once per frame
	void Update()
    {
		m_ablityCoolDown -= Time.deltaTime;

		if(m_ablityCoolDown <= 0)
		{
			m_abilityIcon.color = Color.white; 
		}
		else 
		{
			m_abilityIcon.color = Color.black;
		}
	}

	public void Use()
	{
		if (m_ablityCoolDown <= 0)
		{
			Instantiate(
			  m_abilityDatas.m_abilityInfometions[m_characterId].m_abilityPrefab,
			  gameObject.transform.position,
			  Quaternion.identity,
			  gameObject.transform
			  );

			m_ablityCoolDown = m_abilityDatas.m_abilityInfometions[m_characterId].m_abilityCoolDown;
		}      
    }
}
