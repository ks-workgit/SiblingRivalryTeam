using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    static bool m_isRounded = false;
    static bool m_isSelectStage = false;

    // 前のシーンに戻る
    public static void BackToBeforeScene()
    {
        // ラウンド中でステージ選択中なら戻らない
        if (m_isRounded && m_isSelectStage)
        {
            return;
        }

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int backIndex = Mathf.Max(currentIndex - 1, 0); // 0より下は行かない
        SceneManager.LoadScene(backIndex);
    }

    public static void SetIsRounded(bool isRounded)
    {
        m_isRounded = isRounded;
    }

    public static void SetIsSelectStage(bool isSelectStage)
    {
        m_isSelectStage = isSelectStage;
    }
}
