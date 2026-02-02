using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveGameScene : MonoBehaviour
{
	const int PlayerAmount = 2;

	int m_readyCount = 0;
	bool m_isCompletion;
	bool m_isStartGame;

	[SerializeField] Image m_readyToFightImage;

    private void Start()
    {
        m_readyToFightImage.enabled = false;

        SceneController.SetIsSelectStage(false);
    }

    // Readyカウントを増やす
    public void Ready()
	{
		m_readyCount++;
	}

	// Readyカウントを減らす
	public void NotReady()
	{
		m_readyCount--;
	}

	private void Update()
	{
		// ２票以上
		if (m_readyCount >= PlayerAmount)
		{
			m_readyToFightImage.enabled = true;
            m_isCompletion = true;
			if (m_isStartGame)
			{
				SceneManager.LoadScene("GameScene");
			}
		}
		else
		{
			m_isCompletion = false;
			m_readyToFightImage.enabled = false;
		}
	}

	// 全員が準備完了したかのフラグを返す
	public bool GetmIsCompletion()
	{
		return m_isCompletion;
	}

	// ゲームスタートのフラグをセット
	public void SetStartGame(bool isStart)
	{
		m_isStartGame = isStart;
	}
}
