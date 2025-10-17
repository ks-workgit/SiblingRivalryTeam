using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
	[SerializeField] CharacterManager m_characterManager;
	Transform m_respawnPos;

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

			m_characterManager.OnIsRespawn();

			Respawn();
		}
	}

	void Respawn()
	{
		//プレイヤーをリスポーン位置にリスポーンさせる
		if (!m_characterManager.GetIsDeth())
		{
			m_characterController.enabled = false;

			this.gameObject.transform.position = m_respawnPos.position;

			m_isKnockDown = false;

			m_characterController.enabled = true;
		}
	}
}
