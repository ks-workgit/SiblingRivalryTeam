using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rank : MonoBehaviour
{
	[Header("UIQÆ ")]
	[SerializeField] private GameObject rankAUI;
	[SerializeField] private GameObject rankBUI;

	private bool isGameEnded = false;

	//‚PP‚ªƒS[ƒ‹‚µ‚½
	public void Player1Gole()
	{
		if (isGameEnded) return;
		isGameEnded = true;
		ShowRankA();
	}

	//2P‚ªƒS[ƒ‹‚µ‚½
	public void  player2Gole()
	{
		if (isGameEnded) return;
		isGameEnded = true;
		ShowRankB();
	}

	public void ShowRankA()
	{
		rankAUI.SetActive(true);
		rankBUI.SetActive(false);
	}

	public void  ShowRankB()
	{
		rankAUI.SetActive(false);
		rankBUI.SetActive(true);
	}
}
