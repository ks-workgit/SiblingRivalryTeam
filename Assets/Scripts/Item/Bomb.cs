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
	Rigidbody m_rigidbody;

	private bool m_createdEffect = false;

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
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{ 

			m_characterManeger = other.GetComponent<CharacterManager>();
			m_rigidbody = other.GetComponent<Rigidbody>();

			m_characterManeger.Damage(DetonationDamage);
			m_rigidbody.velocity = KnockBackPower;
		}
	}

	IEnumerator Detonation()
	{
		for (var i = 0; i < 20; i++)
		{
			yield return null;
		}
		m_collider.enabled = false;
		Destroy(gameObject);
	}	
}
