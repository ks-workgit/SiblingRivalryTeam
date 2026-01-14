using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MoveRuleSlectScene : MonoBehaviour
{
	private InputAction m_anyButtonAction;

	private void Awake()
	{
		m_anyButtonAction = new InputAction(
			"AnyButton",
			InputActionType.Button,
			"<Gamepad>/<Button>"
			);

		m_anyButtonAction.performed += OnAnyButton;
	}

	private void OnDestroy()
	{
		m_anyButtonAction.performed -= OnAnyButton;
	}

	private void OnEnable()
	{
		// Action‚Ì—LŒø‰»
		m_anyButtonAction.Enable();
	}

	private void OnDisable()
	{
		// Action‚Ì–³Œø‰»
		m_anyButtonAction.Disable();
	}

	void OnAnyButton(InputAction.CallbackContext context)
	{
		SceneManager.LoadScene("RuleSettings");
	}
}
