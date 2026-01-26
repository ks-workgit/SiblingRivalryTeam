using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilenceDagger : MonoBehaviour
{
	const float CoolTime = 10;

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
			Silence();
		}
    }

	//当たった敵のスキルを使用不可にする
	void Silence()
	{
		GameObject enemy = m_weapon.GetEnemy().gameObject;

		UseAbility enemyUseAbility = enemy.GetComponent<UseAbility>();

		enemyUseAbility.SetAblityCoolDown(CoolTime);
	}
}
