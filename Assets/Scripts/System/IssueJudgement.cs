using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IssueJudgement : MonoBehaviour
{
	[SerializeField] GameObject m_resultScreen;
	[SerializeField] Image m_winnerImage;
	[SerializeField] CharacterDatas characterDatas;
	[SerializeField] RuleSettingsData m_ruleSettingsData;

    [SerializeField] Sprite[] m_winnerSprites;

	AudioSource m_audioSource;

	int m_victoryCrownCountIndex;
	int m_remainingLifeIndex;

	bool m_isEnd;
	bool m_isVictory1P;
	bool m_isVictory2P;

	// Start is called before the first frame update
	void Start()
    {
        m_audioSource = GetComponent<AudioSource>();


        for (int i = 0; i < characterDatas.CrownCount.Length; i++)
		{
			characterDatas.CrownCount[i] = 0;
			characterDatas.IsDeth[i] = false;
		}
	}

    // Update is called once per frame
    void Update()
    {
		if(!m_isEnd)
		{
			Judgement();
		}
	}

	void Judgement()
	{
		//王冠を規定数集めたら勝利
		if (characterDatas.CrownCount[0] >= m_ruleSettingsData.VictoryCrownCount[m_ruleSettingsData.m_crownCount])
		{
			m_winnerImage.sprite = m_winnerSprites[0];

			Debug.Log("かち王冠で");

			m_isEnd = true;
			m_isVictory1P = true;

            m_audioSource.Play();
        }
		//残機を減らしたら勝利
		else if (characterDatas.IsDeth[1])
		{
			m_winnerImage.sprite = m_winnerSprites[0];

			Debug.Log("かち残機なくして");

			m_isEnd = true;
            m_isVictory1P = true;

            m_audioSource.Play();
        }

		//王冠を規定数集めたら勝利
		if (characterDatas.CrownCount[1] >= m_ruleSettingsData.VictoryCrownCount[m_ruleSettingsData.m_crownCount])
		{
			m_winnerImage.sprite = m_winnerSprites[1];

			Debug.Log("かち王冠で");

			m_isEnd = true;
			m_isVictory2P = true;

            m_audioSource.Play();
        }
		//残機を減らしたら勝利
		else if (characterDatas.IsDeth[0])
		{
			m_winnerImage.sprite = m_winnerSprites[1];

			Debug.Log("かち残機なくして");

			m_isEnd = true;
            m_isVictory2P = true;

            m_audioSource.Play();
        }
	}

	// 勝った方を取得する
	public bool GetVictoryPlayer(int player)
	{
		if (player == 1)
		{
			return m_isVictory1P;
		}
		else
		{
			return m_isVictory2P;
		}
	}
}
