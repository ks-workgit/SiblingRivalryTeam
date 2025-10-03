using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverdriveMode : MonoBehaviour
{
	[SerializeField] CharacterManeger m_characterManeger;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			UseSkill();
		}
	}

	void UseSkill()
	{
		int attackDamage = m_characterManeger.GetSetAtttackDamage;
		int attackSpeed = m_characterManeger.GetSetAtttackSpeed;

		attackDamage *= 2;
		attackSpeed *= 2;

		m_characterManeger.GetSetAtttackDamage = attackDamage;
		m_characterManeger.GetSetAtttackSpeed = attackSpeed;

		Debug.Log("オーバードライブモード使用");
	}
}
