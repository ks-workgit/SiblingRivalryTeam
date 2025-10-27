using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	const float TimeLimit = 2;//爆弾が爆発するまでの制限時間
	const int DetonationDamage = 50;
	Vector3 KnockBackPower = new Vector3(0, 15, 0);

	float m_timeLimit;  //爆弾が爆発するまでの制限時間カウント用

	[SerializeField] SphereCollider m_collider;
	[SerializeField] GameObject m_bombEffect;

	CharacterManager m_characterManeger;
	PlayerController m_playerController;

	AudioSource m_audioSource;

	private bool m_createdEffect = false;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
		m_timeLimit += Time.deltaTime;

		//既定の時間を超えたら爆発
		if(m_timeLimit > TimeLimit)
		{
			m_collider.enabled = true;
			Debug.Log("爆発");
			if(!m_createdEffect)
			{
				Instantiate(m_bombEffect,gameObject.transform.position,Quaternion.identity);

				m_createdEffect = true;

                m_audioSource.Play();
            }

			StartCoroutine(Detonation());
		}		
    }
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{ 
			m_characterManeger = other.GetComponent<CharacterManager>();
			m_playerController = other.GetComponent<PlayerController>();

			m_characterManeger.Damage(DetonationDamage);
		}
	}

	IEnumerator Detonation()
	{
		yield return new WaitForSeconds(1);
		m_collider.enabled = false;
		Destroy(gameObject);
	}	
}
