using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
	const float DestoryTime = 10.0f;
	const int Damage = 20;

	GameObject m_player;

	bool m_isGround;
	float m_destoryTime;

	GeoLevitate m_geoLevitate;

	public void SetPlayer(GameObject player)
	{
		m_player = player;
	}

	public void SetGeoLevitate(GeoLevitate geoLevitate)
	{
		m_geoLevitate = geoLevitate;
	}


	void Update()
    {
        if(m_isGround)
		{
			m_destoryTime += Time.deltaTime;
		}

		if (m_destoryTime > DestoryTime)
		{
			Destroy(gameObject);
		}		
    }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player" && !m_isGround)
		{
			if (collision.gameObject == m_player) return;

			CharacterManager characterManager = collision.gameObject.GetComponent<CharacterManager>();

			characterManager.Damage(Damage);

			//岩がプレイヤーにあった場合は岩を一つ復活
			m_geoLevitate.RockHit();
		}

		//地面に当たった岩はプレイヤーにダメージを与えないように
		if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			m_isGround = true;
		}
	}
}
