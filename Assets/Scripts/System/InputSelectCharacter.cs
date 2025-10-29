using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InputSelectCharacter : MonoBehaviour
{
    SelectCharacter m_selectCharacter;
    PlayerInput m_playerInput;

    //[SerializeField] Button[] m_buttons;

    public void SetButton(Button[] buttons)
    {
        //m_buttons = buttons;
    }

    private void Awake()
    {
        m_selectCharacter = GetComponent<SelectCharacter>();
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
        m_selectCharacter.RightButtonOnClick();

        //m_buttons[0].onClick.AddListener(() => Debug.Log("test"));
    }

    private void OnLeft(InputAction.CallbackContext callback)
    {
        m_selectCharacter.LeftButtonOnClick();
    }

    private void OnReady(InputAction.CallbackContext callback)
    {
        m_selectCharacter.ReadyOnclick();
    }

    public void SetSelectCharacter(SelectCharacter selectCharacter)
    {
        m_selectCharacter = selectCharacter;
    }
}
