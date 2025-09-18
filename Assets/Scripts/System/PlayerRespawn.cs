using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
	[SerializeField] Transform m_respawnPos;
	[SerializeField] CharacterManeger m_characterManeger;
	GameObject DethPlayer;

	bool m_isKnockDown;
	Rigidbody m_rigidbody;

	// Start is called before the first frame update
	void Start()
    {
		m_rigidbody = GetComponent<Rigidbody>();
		DethPlayer = gameObject;
	}

    // Update is called once per frame
    void Update()
	{
		if (m_characterManeger.GetHelth() <= 0)
		{
			m_isKnockDown = true;

			m_characterManeger.OnIsRespawn();
		}

		if (m_isKnockDown)
		{
			//���Ԃ������J���ă��X�|�[��������
			Invoke("Respawn", 2);
		}
    }

	void Respawn()
	{
		//�v���C���[�����X�|�[���ʒu�Ƀ��X�|�[��������
		if (!m_characterManeger.GetIsDeth())
		{
			DethPlayer.transform.position = m_respawnPos.position;

			m_rigidbody.velocity = Vector3.zero;

			m_isKnockDown = false;
		}
	}
}
