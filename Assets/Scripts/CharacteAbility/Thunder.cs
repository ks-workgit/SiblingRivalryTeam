using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
	CharacterManager m_characterManeger;

	int m_damage = 75;
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			m_characterManeger = other.GetComponent<CharacterManager>();

			if (other.TryGetComponent(out PlayerController playerController))
			{
				if (!playerController.GetIsInvincible())
				{
					playerController.SetIsStun(true);
                    m_characterManeger.Damage(m_damage);					
				}
			}
		}
	}
}
