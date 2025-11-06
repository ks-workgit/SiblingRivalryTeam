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

    static int[] RoundsCount = { 1, 2, 3 }; // ラウンド数

    // 現在のラウンド数
    public static int Round1P = 0;
    public static int Round2P = 0;

    bool m_isFinished = false;

    const float SceneLoadTime = 3.0f;   // シーン遷移するまでの時間

    private void Start()
    {
        m_roundPanel.SetActive(false);
    }

    private void Update()
    {
        if (!m_isFinished)
        {
            // 1Pの勝利
            if (m_issueJudgement.GetVictoryPlayer(1))
            {
                WhichVictory(1);
            }
            // 2Pの勝利
            else if (m_issueJudgement.GetVictoryPlayer(2))
            {
                WhichVictory(2);
            }
        }
    }

    // 勝った方のカウントを増やす
    private void WhichVictory(int player)
    {
        if (player == 1)
        {
            Round1P++;            
        }
        else
        {
            Round2P++;            
        }

        m_roundCount1P.text = Round1P.ToString();
        m_roundCount2P.text = Round2P.ToString();

        m_roundPanel.SetActive(true);
        m_isFinished = true;

        StartCoroutine(SceneLoad());
    }

    IEnumerator SceneLoad()
    {
        yield return new WaitForSeconds(SceneLoadTime);

        // 現在のラウンド数が設定したラウンド数以上になったか
        if (Round1P >= RoundsCount[1] || Round2P >= RoundsCount[1])
        {
            Round1P = 0;
            Round2P = 0;
            SceneManager.LoadScene("Result");
        }
        else
        {
            SceneManager.LoadScene("Stagechoice");
        }

        m_roundPanel.SetActive(false);
        m_isFinished = false;
    }
}
