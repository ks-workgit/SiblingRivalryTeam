using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSelectStage : MonoBehaviour
{
    [SerializeField] MapSelectUI m_mapSelectUI;
    PlayerInput m_playerInput;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        m_playerInput.actions["RightSelect"].performed += OnRight;
        m_playerInput.actions["LeftSelect"].performed += OnLeft;
        m_playerInput.actions["Ready"].performed += OnReady;
    }

    private void OnDisable()
    {
        m_playerInput.actions["RightSelect"].performed -= OnRight;
        m_playerInput.actions["LeftSelect"].performed -= OnLeft;
        m_playerInput.actions["Ready"].performed -= OnReady;
    }

    private void OnRight(InputAction.CallbackContext callback)
    {
        m_mapSelectUI.NextMap();
    }

    private void OnLeft(InputAction.CallbackContext callback)
    {
        m_mapSelectUI.PrevMap();
    }

    private void OnReady(InputAction.CallbackContext callback)
    {
        m_mapSelectUI.StartGame();
    }
}
