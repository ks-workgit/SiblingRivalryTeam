using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomTrade : MonoBehaviour
{
	[SerializeField] GameObject m_effect;
	GameObject m_player;

	AudioSource m_se;
	PlayerController m_characterController;
	UseAbility m_useAbility;

	bool m_isUse;

	private void Start()
	{
		m_player = transform.parent.gameObject;

        m_se = GetComponent<AudioSource>();	
		m_characterController = m_player.GetComponent<PlayerController>();
		m_useAbility = m_player.GetComponent<UseAbility>();
    }

	private void Update()
	{
		if (m_isUse)
		{
			if (!m_se.isPlaying)
			{
				Destroy(gameObject);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!m_characterController.GetIsGrounded())
		{
			m_useAbility.ResetIsUse();

			Destroy(gameObject);
		}
		else if (other.CompareTag("Player") && !m_isUse)
		{
			if (other.transform.position == m_player.transform.position) return;

            m_se.Play();

            CharacterController enemy = other.GetComponent<CharacterController>();
			CharacterController player = m_player.GetComponent<CharacterController>();

			enemy.enabled = false;
			player.enabled = false;

			Vector3 enemyPosition = other.transform.position;
			Vector3 playerPosition = m_player.transform.position;

			Debug.Log("自身" + playerPosition + "相手" + enemyPosition);

			m_player.transform.position = enemyPosition;
			other.transform.position = playerPosition;

			//エフェクト生成
			Instantiate(m_effect, transform.position, Quaternion.identity, transform);
			Instantiate(m_effect, other.transform.position, Quaternion.identity, other.transform);			

			Debug.Log("ファントムトレード発動");
			Debug.Log("自身" + m_player.transform.position + "相手" + other.transform.position);

			enemy.enabled = true;
			player.enabled = true;

			m_isUse = true;			
		}
	}
}
