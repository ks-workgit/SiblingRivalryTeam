using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class LocalMultiUISetup : MonoBehaviour
{
    [SerializeField] PlayerInputManager m_playerInputManager;
    [SerializeField] SelectCharacter[] m_selectCharacter;

    int m_createPlayerCount = 0;

    private void Start()
    {
        var devices = InputSystem.devices;

        foreach (var device in devices)
        {
            if (device is Gamepad)
            {
                Gamepad gamepad = device as Gamepad;
                Debug.Log($"コントローラー検出: {gamepad.displayName}");

                OnPlayerJoined(m_createPlayerCount, gamepad);

                m_createPlayerCount++;
            }
        }
    }

    // プレイヤーが入室したときの処理
    private void OnPlayerJoined(int playerId, Gamepad gamePad)
    {
        PlayerInput player = m_playerInputManager.JoinPlayer(-1, -1, null, gamePad);

        InputSelectCharacter inputSelectCharacter = player.GetComponent<InputSelectCharacter>();
        inputSelectCharacter.SetSelectCharacter(m_selectCharacter[playerId]);
    }
}
