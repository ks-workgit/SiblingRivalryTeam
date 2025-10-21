using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	const float TimeLimit = 2;//”š’e‚ª”š”­‚·‚é‚Ü‚Å‚Ì§ŒÀŽžŠÔ
	const int DetonationDamage = 50;
	Vector3 KnockBackPower = new Vector3(0, 15, 0);

	float m_timeLimit;  //”š’e‚ª”š”­‚·‚é‚Ü‚Å‚Ì§ŒÀŽžŠÔƒJƒEƒ“ƒg—p

	[SerializeField] SphereCollider m_collider;
	[SerializeField] GameObject m_bombEffect;

	CharacterManager m_characterManeger;
	CharacterController m_characterController;
	PlayerController m_playerController;

	private bool m_createdEffect = false;

	bool m_isHit;

	void Update()
    {
		m_timeLimit += Time.deltaTime;

		//Šù’è‚ÌŽžŠÔ‚ð’´‚¦‚½‚ç”š”­
		if(m_timeLimit > TimeLimit)
		{
			m_collider.enabled = true;
			Debug.Log("”š”­");
			if(!m_createdEffect)
			{
				Instantiate(m_bombEffect,gameObject.transform.position,Quaternion.identity);

				m_createdEffect = true;
			}

			StartCoroutine(Detonation());
		}		
    }
	
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{ 
			m_characterManeger = other.GetComponent<CharacterManager>();
			m_playerController = other.GetComponent<PlayerController>();

			if (!m_isHit)
			{
				m_characterManeger.Damage(DetonationDamage);
			}

			m_playerController.KnockBack(KnockBackPower.y);

			m_isHit = true;
		}
	}

	IEnumerator Detonation()
	{
		yield return new WaitForSeconds(1);
		m_collider.enabled = false;
		Destroy(gameObject);
	}	
}
