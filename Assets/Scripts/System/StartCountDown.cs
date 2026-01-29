using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartCountDown : MonoBehaviour
{
    public static StartCountDown Instance { get; private set; }
    
    [SerializeField] TextMeshProUGUI m_countDownText;
    int m_count = 3;
    bool m_isActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private IEnumerator Start()
    {
        yield return CountDown();
        Destroy(gameObject);
    }

    IEnumerator CountDown()
    {
        // ƒJƒEƒ“ƒg‚ð1•b‚²‚Æ‚ÉŒ¸‚ç‚·
        while (m_count >= 0)
        {
            yield return new WaitForSeconds(1.0f);
            m_count--;
        }
    }

    private void Update()
    {
        m_countDownText.text = m_count.ToString();

        if (m_count == 0)
        {
            m_countDownText.text = "GO!!".ToString();
            m_isActive = true;
        }
    }

    public bool GetIsActive()
    {
        return m_isActive;
    }
}
