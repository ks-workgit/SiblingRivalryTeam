using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbility : MonoBehaviour
{
	[SerializeField] AbilityDatas m_abilityDatas;

	[SerializeField] int m_characterId;

	float m_ablityCoolDown;

	private void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
    {
		m_ablityCoolDown -= Time.deltaTime;
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
