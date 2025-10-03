using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IssueJudgement : MonoBehaviour
{
	[SerializeField] CharacterManeger m_characterManeger;
	[SerializeField] GameObject m_resultScreen;

	static int[] VectoryCrownCount = { 3, 5, 10 };

	int m_crownCount;
	bool m_isDeth;

	int m_vectoryCrownCountIndex;
	int m_remainingLifeIndex;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		m_crownCount = m_characterManeger.GetCrownCount();
		m_isDeth = m_characterManeger.GetIsDeth();

		//王冠を規定数集めたら勝利
		if (m_crownCount >= VectoryCrownCount[0])
		{
			m_resultScreen.SetActive(true);

			Debug.Log("かち王冠で");
		}
		//残機を減らしたら勝利
		else if(m_isDeth)
		{
			m_resultScreen.SetActive(true);

			Debug.Log("かち残機なくして");
		}
    }
}
