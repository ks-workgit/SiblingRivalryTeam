using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputNavigation : MonoBehaviour
{
    [SerializeField] RuleSettings m_ruleSettings;

    float m_inputTime;
    float m_delay = 0.2f;

    // UIの移動
    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        var inputValue = context.ReadValue<Vector2>();

        // 一定間隔をもたせる
        if (Time.time -  m_inputTime > m_delay)
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

    // 左右入力に応じて処理
    private void HandleHorizontalInput(int direction)
    {
        // 選択中の項目の数値を増減させる
        if (m_ruleSettings.GetRoundSelect())
        {
            m_ruleSettings.SetRoundCount(direction);
        }
        else if (m_ruleSettings.GetLifeSelect())
        {
            m_ruleSettings.SetLifeCount(direction);
        }
        else if (m_ruleSettings.GetCrownSelect())
        {
            m_ruleSettings.SetCrownCount(direction);
        }

        m_inputTime = Time.time;
    }
}
