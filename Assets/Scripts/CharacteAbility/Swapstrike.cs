using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapstrike : MonoBehaviour
{
	TakeWeapon m_enemyWeapon;
	TakeWeapon m_playerWeapon;

    // Start is called before the first frame update
    void Start()
    {
        m_playerWeapon = transform.parent.gameObject.GetComponent<TakeWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			if (other.gameObject == transform.parent.gameObject) return;

			m_enemyWeapon = other.GetComponent<TakeWeapon>();

			int enemyWeaponId = m_enemyWeapon.GetHaveWeaponId();

			m_enemyWeapon.GettingWeapon(m_playerWeapon.GetHaveWeaponId());

			m_playerWeapon.GettingWeapon(enemyWeaponId);

			Destroy(gameObject);
		}
	}
}
