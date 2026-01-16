using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseAbility : MonoBehaviour
{
	[SerializeField] AbilityDatas m_abilityDatas;
	[SerializeField] int m_characterId;

	Image m_abilityIcon;
	GameObject m_abilityObject;

	float m_ablityCoolDown;

	bool m_isUse;
	bool m_reactivation;

	public bool GetReactivation()
	{
		return m_reactivation;
	}

	public void ResetReactivation()
	{
		m_reactivation = false;
	}

	public void ResetIsUse()
	{
		m_isUse = false;
	}

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

		if(!m_abilityObject && m_isUse)
		{
			m_ablityCoolDown = m_abilityDatas.m_abilityInfometions[m_characterId].m_abilityCoolDown;

			m_isUse = false;
		}
	}

	public void Use()
	{
		if (m_ablityCoolDown <= 0 && !m_isUse && m_characterId != 4)
		{
			m_abilityObject = Instantiate(
			  m_abilityDatas.m_abilityInfometions[m_characterId].m_abilityPrefab,
			  gameObject.transform.position,
			  Quaternion.identity,
			  gameObject.transform
			  );

			m_abilityObject.transform.localRotation = Quaternion.identity;

			m_isUse = true;
		}
		else if(m_isUse)
		{
			m_reactivation = true;
		}
    }
}
