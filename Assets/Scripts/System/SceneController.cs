using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    static bool m_canBack = true;

    // 前のシーンに戻る
    public static void BackToBeforeScene()
    {
        if (!m_canBack) return;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int backIndex = Mathf.Max(currentIndex - 1, 0); // 0より下は行かない
        SceneManager.LoadScene(backIndex);
    }

    // 前のシーンに戻れるかのフラグをセットする
    public static void SetCanBack(bool canBack)
    {
         m_canBack = canBack;
    }
}
