using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InputSelectCharacter : MonoBehaviour
{
    [SerializeField] SelectCharacter m_selectCharacter;

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

        Debug.Log(inputValue.x);
    }

    // ¶‰E“ü—Í‚É‰ž‚¶‚Äˆ—
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
