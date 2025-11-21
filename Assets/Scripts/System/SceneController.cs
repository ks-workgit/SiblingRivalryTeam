using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    public static void BackToBeforScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int backIndex = Mathf.Max(currentIndex - 1, 0); // 0ÇÊÇËâ∫ÇÕçsÇ©Ç»Ç¢
        SceneManager.LoadScene(backIndex);
    }
}
