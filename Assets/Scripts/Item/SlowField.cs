using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowField : MonoBehaviour
{
	const float DestroyTime = 15.0f;	//消えるまでの時間
	float ChageMagnification = 0.5f;    //変更する倍率

	List<PlayerController> m_touchedPlayer = new List<PlayerController>();

	PlayerController m_playerController;

	float m_destroyTime = DestroyTime;

	private void Update()
	{
		m_destroyTime -= Time.deltaTime;

		if(m_destroyTime <= 0 )
		{
			SpeedReset();

			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		//プレイヤーが当たっているなら足の速さを下げる
		if(other.gameObject.CompareTag("Player"))
		{
			PlayerController playerController = other.GetComponent<PlayerController>();

            playerController.GetSetSpeedMagnification -= ChageMagnification;

            playerController.ChangeSpeed();

			m_touchedPlayer.Add(playerController);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		//出たら足の速さをもとに戻す
		if (other.gameObject.CompareTag("Player"))
		{
			m_playerController = other.GetComponent<PlayerController>();

			m_playerController.GetSetSpeedMagnification += ChageMagnification;
			m_playerController.ChangeSpeed();

			for (int i = 0; i < m_touchedPlayer.Count; i++)
			{
				if(m_touchedPlayer[i].gameObject == other.gameObject)
				{
					m_touchedPlayer.RemoveAt(i);
				}
			}
		}
	}

	void SpeedReset()
	{
		for (int i = 0;i < m_touchedPlayer.Count; i++)
		{
			m_playerController = m_touchedPlayer[i].GetComponent<PlayerController>();

			m_playerController.GetSetSpeedMagnification += ChageMagnification;
			m_touchedPlayer[i].ChangeSpeed();
		}
	}
}
