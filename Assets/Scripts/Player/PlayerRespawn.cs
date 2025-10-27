using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRespawn : MonoBehaviour
{
	[SerializeField] CharacterManager m_characterManager;
	[SerializeField] PlayerController m_playerController;
	Transform m_respawnPos;

	bool m_isKnockDown;
	bool m_isRespawn;

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

			m_characterManager.OnIsRespawn();

			Respawn();

			m_isRespawn = true;
		}

		if(m_playerController.GetIsAny())
		{
			m_isRespawn = false;

			m_playerController.SetIsAny();
		}

		if(m_isRespawn)
		{
			m_playerController.NowRespawn();			
		}

		Debug.Log("isRespawn " + m_isRespawn);
	}

	void Respawn()
	{
		//プレイヤーをリスポーン位置にリスポーンさせる
		if (!m_characterManager.GetIsDeth())
		{
			Vector3 respawnPos = new Vector3(m_respawnPos.position.x, m_respawnPos.position.y + 1, m_respawnPos.position.z);

			m_characterController.enabled = false;

			this.gameObject.transform.position = respawnPos;

			m_isKnockDown = false;

			m_playerController.OnIsGrounded();

			m_characterController.enabled = true;
		}
	}
}
