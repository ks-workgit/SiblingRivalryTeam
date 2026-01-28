using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostSword : MonoBehaviour
{
	const float OffSet = 1.5f;

	Vector3 CrownOffSet = new Vector3 (0,2,0);

	[SerializeField] CharacterDatas m_characterDatas;
	[SerializeField] GameObject m_crownPrefab;

	PlayerController m_playerController;
	Weapon m_weapon;
	GameObject m_owner;
	GameObject m_enemy;

	private void Start()
	{
		m_weapon = GetComponent<Weapon>();
		m_owner = m_weapon.GetOwner();
	}

	private void Update()
	{
		//武器から当たったと返って来たら
		if(m_weapon.GetHit())
		{
			m_playerController = m_weapon.GetEnemy().GetComponent<PlayerController>();

			if (!m_playerController.GetIsGrounded())
			{
				SteeleCrown();
			}
		}
	}

	//攻撃があった場合に王冠を落とされる
	void SteeleCrown()
	{
		CharacterManager characterManager = m_owner.GetComponent<CharacterManager>();
		m_enemy = m_weapon.GetEnemy().gameObject;

		int ownerId = characterManager.GetPlayerId();
		int enemyId = 0;
		if (ownerId == 0)
		{
			enemyId = 1;
		}

		if (m_characterDatas.CrownCount[enemyId] > 0)
		{
			//プレイヤーの前方の位置を計算
			Vector3 playerFrontPos = new Vector3(
				transform.position.x + transform.forward.x,
				transform.position.y + transform.forward.y + OffSet,
				transform.position.z + transform.forward.z);

			GameObject dropCrown = Instantiate(
				m_crownPrefab,
				m_enemy.transform.position + CrownOffSet,
				Quaternion.Euler(-90, 0, 0));

			Rigidbody crownRb = dropCrown.GetComponent<Rigidbody>();
			//落とした王冠を抜いてる方向に飛ばす
			crownRb.velocity = new Vector3(transform.forward.x * 7, transform.forward.y * 7, transform.forward.z * 7);			

			//相手の王冠の数を減らす
			m_characterDatas.CrownCount[enemyId]--;
		}
	}
}
