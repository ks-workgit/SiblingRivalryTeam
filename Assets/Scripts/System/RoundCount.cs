using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_roundCount1P, m_roundCount2P;
    [SerializeField] GameObject m_roundPanel;
    [SerializeField] IssueJudgement m_issueJudgement;

    public static int m_round1P;
    public static int m_round2P;

    bool m_isFinished = false;

    const float SceneLoadTime = 3.0f;

    private void Start()
    {
        m_roundPanel.SetActive(false);
    }

    private void Update()
    {

        if (!m_isFinished)
        {
            // 1PÇÃèüóò
            if (m_issueJudgement.GetVictoryPlayer(1))
            {
                WhichVictory(1);
            }
            // 2PÇÃèüóò
            else if (m_issueJudgement.GetVictoryPlayer(2))
            {
                WhichVictory(2);
            }
        }

    }

    private void WhichVictory(int player)
    {
        if (player == 1)
        {
            m_round1P++;            
        }
        else
        {
            m_round2P++;            
        }

        m_roundCount1P.text = m_round1P.ToString();
        m_roundCount2P.text = m_round2P.ToString();

        m_roundPanel.SetActive(true);
        m_isFinished = true;

        StartCoroutine(SceneLoad());
    }

    IEnumerator SceneLoad()
    {
        Debug.Log(SceneLoadTime);
        yield return new WaitForSeconds(SceneLoadTime);
        SceneManager.LoadScene("Stagechoice");
        m_roundPanel.SetActive(false);
        m_isFinished = false;
    }
}
