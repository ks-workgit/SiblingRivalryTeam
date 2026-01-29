using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
	[SerializeField] CharacterManager m_characterManager;
	[SerializeField] PlayerController m_playerController;

	[SerializeField] GameObject m_crownPrefab;
	[SerializeField] CharacterDatas m_characterDatas;

	[SerializeField] GameObject m_dethEffect;
	[SerializeField] GameObject m_floorPrefab;

	Transform m_respawnPos;
	GameObject m_floorGameObject;

	bool m_isKnockDown;

	CharacterController m_characterController;

	public void SetRespawnPos(Transform respawnPos)
	{
		m_respawnPos = respawnPos;
	}

	public bool GetIsKnockDown()
	{
		return m_isKnockDown;
	}

	// Start is called before the first frame update
	void Start()
	{
		m_characterController = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		if (m_characterManager.GetHelth() <= 0)
		{
			m_isKnockDown = true;

			m_playerController.IceMelts();
			m_characterManager.OnIsRespawn();

			DropCrown(transform.position);

			Respawn();
		}
	}

	//死亡したときに王冠を落とす
	void DropCrown(Vector3 playerPos)
	{
		if (m_characterDatas.CrownCount[m_characterManager.GetPlayerId()] > 0)
		{
			Instantiate(m_crownPrefab, playerPos, Quaternion.Euler(-90, 0, 0));

			m_characterDatas.CrownCount[m_characterManager.GetPlayerId()]--;
		}
	}

	void Respawn()
	{
		//プレイヤーをリスポーン位置にリスポーンさせる
		if (!m_characterManager.GetIsDeth())
		{
			Vector3 respawnPos = new Vector3(m_respawnPos.position.x, m_respawnPos.position.y + 1, m_respawnPos.position.z);

			Instantiate(
				m_dethEffect,
				transform.position,
				Quaternion.identity);

			m_characterController.enabled = false;

			m_floorGameObject = Instantiate(
				m_floorPrefab,
				m_respawnPos.position,
				Quaternion.identity);

			this.gameObject.transform.position = respawnPos;

			//キャラクターの向きを正面に
			transform.rotation = Quaternion.Euler(0f, 90f, 0f);

			m_isKnockDown = false;

			m_playerController.OnIsGrounded();

			m_characterController.enabled = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("RespawnFloor"))
		{
			Destroy(m_floorGameObject);
		}
	}
}
