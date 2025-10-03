using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ShowPlayerLife : MonoBehaviour
{
	[SerializeField] CharacterManeger characterManeger;
	[SerializeField] GameObject m_remainingLifeUi;

	int m_remainingLife;

	// Start is called before the first frame update
	void Start()
    {
		m_remainingLife = characterManeger.GetRemainingLife();

		CreateRemainingLifeUi(m_remainingLife);
	}

	void CreateRemainingLifeUi(int remainingLife)
	{
		//c‹@‚ğŠù’è‚Ì”¶¬
		for (int i = 0; i < remainingLife; i++)
		{
			Instantiate(
			m_remainingLifeUi,
			new Vector3(transform.position.x + 150 * i,
			transform.position.y,
			transform.position.z),
			Quaternion.identity,
			transform
			);
		}
	}
}
