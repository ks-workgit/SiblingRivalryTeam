using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenBlade : MonoBehaviour
{
	const float MeltsTime = 5f;

	Weapon m_weapon;

    // Start is called before the first frame update
    void Start()
    {
		m_weapon = GetComponent<Weapon>();
	}

    // Update is called once per frame
    void Update()
    {
        if(m_weapon.GetHit())
		{
			Frozen();
		}
    }

	//‘Šè‚ÉUŒ‚‚ª“–‚½‚ê‚Î‘Šè‚ğ“€‚ç‚¹‚é‚±‚Æ‚ª‚Å‚«‚é
	void Frozen()
	{
		GameObject enemy = m_weapon.GetEnemy().gameObject;

		PlayerController playerController = enemy.GetComponent<PlayerController>();

		playerController.SetIsFreeze(MeltsTime);
	}
}
