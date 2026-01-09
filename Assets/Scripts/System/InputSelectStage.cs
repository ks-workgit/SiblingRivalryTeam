using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSelectStage : MonoBehaviour
{
    [SerializeField] MapSelectUI m_mapSelectUI;

    float m_inputTime;
    float m_delay = 0.2f;

    // UI‚ÌˆÚ“®
    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        var inputValue = context.ReadValue<Vector2>();

        // ˆê’èŠÔŠu‚ð‚à‚½‚¹‚é
        if (Time.time - m_inputTime > m_delay)
        {
            // ¶‰E‚Ì“ü—Í
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

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        SceneController.BackToBeforeScene();
    }

    // ¶‰E“ü—Í‚É‰ž‚¶‚Äˆ—
    private void HandleHorizontalInput(int direction)
    {
        if (direction > 0)
        {
            m_mapSelectUI.NextMap();
        }
        else if (direction < 0)
        {
            m_mapSelectUI.PrevMap();
        }

        m_inputTime = Time.time;
    }
}
