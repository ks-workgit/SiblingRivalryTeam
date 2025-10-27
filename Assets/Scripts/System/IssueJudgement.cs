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

	[SerializeField] Sprite[] m_winnerSprites;

	static int[] VectoryCrownCount = { 3, 5, 10 };

	AudioSource m_audioSource;

	int m_vectoryCrownCountIndex;
	int m_remainingLifeIndex;

	bool m_isEnd;

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
		if (characterDatas.CrownCount[0] >= VectoryCrownCount[0])
		{
			m_resultScreen.SetActive(true);

			m_winnerImage.sprite = m_winnerSprites[0];

			Debug.Log("かち王冠で");

			m_isEnd = true;

            m_audioSource.Play();
        }
		//残機を減らしたら勝利
		else if (characterDatas.IsDeth[1])
		{
			m_resultScreen.SetActive(true);

			m_winnerImage.sprite = m_winnerSprites[0];

			Debug.Log("かち残機なくして");

			m_isEnd = true;

            m_audioSource.Play();
        }

		//王冠を規定数集めたら勝利
		if (characterDatas.CrownCount[1] >= VectoryCrownCount[0])
		{
			m_resultScreen.SetActive(true);

			m_winnerImage.sprite = m_winnerSprites[1];

			Debug.Log("かち王冠で");

			m_isEnd = true;

            m_audioSource.Play();
        }
		//残機を減らしたら勝利
		else if (characterDatas.IsDeth[0])
		{
			m_resultScreen.SetActive(true);

			m_winnerImage.sprite = m_winnerSprites[1];

			Debug.Log("かち残機なくして");

			m_isEnd = true;

            m_audioSource.Play();
        }
	}
}
