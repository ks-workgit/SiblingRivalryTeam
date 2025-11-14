using UnityEngine;
using UnityEngine.InputSystem;

public class AssignGamepads : MonoBehaviour
{
    [SerializeField] PlayerInput m_player1;
    [SerializeField] PlayerInput m_player2;

    void Start()
    {
        // 使用されているゲームパッド一覧を取得
        var gamepads = Gamepad.all;

        if (gamepads.Count >= 1)
        {
            m_player1.SwitchCurrentControlScheme("Gamepad", gamepads[0]);
        }

        if (gamepads.Count >= 2)
        {
            m_player2.SwitchCurrentControlScheme("Gamepad", gamepads[1]);
        }
    }
}
