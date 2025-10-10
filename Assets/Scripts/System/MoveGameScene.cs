using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveGameScene : MonoBehaviour
{
	const int PlayerAmount = 2;

	int m_readyCount = 0;

	public void Ready()
	{
		m_readyCount++;
	}

	public void NotReady()
	{
		m_readyCount--;
	}

	private void Update()
	{
		if(m_readyCount >= PlayerAmount)
		{
			SceneManager.LoadScene("GameScene");
		}
	}
}
