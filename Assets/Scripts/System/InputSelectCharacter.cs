using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InputSelectCharacter : MonoBehaviour
{
    [SerializeField] SelectCharacter m_selectCharacter;
    [SerializeField] MoveGameScene m_moveGameScene;

    float m_inputTime;
    float m_delay = 0.2f;

    // UIの移動
    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        var inputValue = context.ReadValue<Vector2>();

        // 一定間隔をもたせる
        if (Time.time - m_inputTime > m_delay)
        {
            // 左右の入力
            if (inputValue.x > 0.5f)
            {
                HandleHorizontalInput(1);
            }
            else if (inputValue.x < -0.5f)
            {
                HandleHorizontalInput(-1);
            }
        }
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // 全員が準備完了したとき
        if (m_moveGameScene.GetmIsCompletion())
        {
            // ゲームスタートフラグをセット
            m_moveGameScene.SetStartGame(true);
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // Readyのとき
        if (m_selectCharacter.GetReady())
        {
            // Readyをfalseに
            m_moveGameScene.NotReady();
            m_selectCharacter.SetIsReady(false);
        }
        else
        {
            // ReadyじゃないときSceneを戻れる
            SceneController.BackToBeforeScene();
        }
    }

    // 左右入力に応じて処理
    private void HandleHorizontalInput(int direction)
    {
        if (direction > 0)
        {
            m_selectCharacter.RightButtonOnClick();
        }
        else if (direction < 0)
        {
            m_selectCharacter.LeftButtonOnClick();
        }

        m_inputTime = Time.time;
    }

    public void SetSelectCharacter(SelectCharacter selectCharacter)
    {
        m_selectCharacter = selectCharacter;
    }
}
