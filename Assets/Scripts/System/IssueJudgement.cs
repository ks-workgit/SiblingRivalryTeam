using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IssueJudgement : MonoBehaviour
{
	[SerializeField] CharacterManeger[] m_characterManeger;
	[SerializeField] GameObject m_resultScreen;
	[SerializeField] TextMeshProUGUI m_winnerText;

	static int[] VectoryCrownCount = { 3, 5, 10 };

	int m_1pCrownCount;
	bool m_1pIsDeth;

	int m_2pCrownCount;
	bool m_2pIsDeth;

	int m_vectoryCrownCountIndex;
	int m_remainingLifeIndex;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		m_1pCrownCount = m_characterManeger[0].GetCrownCount();
		m_1pIsDeth = m_characterManeger[0].GetIsDeth();

		m_2pCrownCount = m_characterManeger[1].GetCrownCount();
		m_2pIsDeth = m_characterManeger[1].GetIsDeth();

		//王冠を規定数集めたら勝利
		if (m_1pCrownCount >= VectoryCrownCount[0])
		{
			m_resultScreen.SetActive(true);

			m_winnerText.text = "1P Win";

			Debug.Log("かち王冠で");
		}
		//残機を減らしたら勝利
		else if(m_1pIsDeth)
		{
			m_resultScreen.SetActive(true);

			m_winnerText.text = "1P Win";

			Debug.Log("かち残機なくして");
		}

		//王冠を規定数集めたら勝利
		if (m_2pCrownCount >= VectoryCrownCount[0])
		{
			m_resultScreen.SetActive(true);

			m_winnerText.text = "2P Win";

			Debug.Log("かち王冠で");
		}
		//残機を減らしたら勝利
		else if (m_2pIsDeth)
		{
			m_resultScreen.SetActive(true);

			m_winnerText.text = "2P Win";

			Debug.Log("かち残機なくして");
		}
	}
}
