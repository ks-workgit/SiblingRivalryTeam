using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IssueJudgement : MonoBehaviour
{
	[SerializeField] CrownCount crownCount;

	static int[] VectoryCrownCount = { 3, 5, 10 };
	static int[] RemainingLife = { 3, 5 };

	int m_crownCount;
	int m_remainingLife;

	int m_vectoryCrownCountIndex;
	int m_remainingLifeIndex;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		m_crownCount = crownCount.GetCrownCount();

		//‰¤Š¥‚ğ‹K’è”W‚ß‚½‚çŸ—˜
		if (m_crownCount == VectoryCrownCount[0])
		{
			Debug.Log("‚©‚¿‰¤Š¥‚Å");
		}
		//c‹@‚ğŒ¸‚ç‚µ‚½‚çŸ—˜
		else if(m_remainingLife == RemainingLife[0])
		{
			Debug.Log("‚©‚¿‘Ì—Í‚È‚­‚µ‚Ä");
		}
    }
}
