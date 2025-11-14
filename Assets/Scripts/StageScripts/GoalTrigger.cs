using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
	[SerializeField] private GameObject resultPanel;
	[SerializeField] private GameObject rankA;   // A用パネル
	[SerializeField] private GameObject rankB;   // B用パネル
	[SerializeField] private int playerNumber;   // 1Pなら1, 2Pなら2
	[SerializeField] private Rank goalManager;   // ゴール管理用スクリプト

	private static bool goalReached = false; // 最初にゴールした人だけ反応させる

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !goalReached)
		{
			goalReached = true; // 1人目がゴールしたらtrue

			resultPanel.SetActive(true); // リザルト画面ON

			if (playerNumber == 1)
			{
				rankA.SetActive(true);
				rankB.SetActive(false);
			}
			else if(playerNumber == 2) 
			{
				rankA.SetActive(false);
				rankB.SetActive(true);
			}
		}
	}
}
