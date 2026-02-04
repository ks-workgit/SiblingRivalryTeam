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
	bool m_sePlaying;
	bool m_activeSePlaying;

	[SerializeField] Image m_readyToFightImage;
	[SerializeField] AudioSource m_startSe;
	[SerializeField] AudioClip m_activeSe,m_readyToFightSe;

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

			if (!m_activeSePlaying)
			{
				m_startSe.PlayOneShot(m_activeSe);
				m_activeSePlaying = true;
			}
			if (m_isStartGame)
			{
				if (!m_sePlaying)
				{
					m_startSe.Play();
					m_startSe.PlayOneShot(m_readyToFightSe);
					Debug.Log("音なった");
					m_sePlaying = true;
				}
				if (!m_startSe.isPlaying)
				{
					SceneManager.LoadScene("GameScene");
				}
			}
		}
		else
		{
			m_isCompletion = false;
			m_readyToFightImage.enabled = false;
			m_activeSePlaying = false;
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

	public bool IsStartGame()
	{
		return m_isStartGame;
	}
}
