using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_speed = 8;
    [SerializeField] float m_jumpPower = 6;

    bool m_isGrounded;

    Vector3 m_direction;
    Vector3 m_veloctiy;

    PlayerInput m_playerInput;
    Rigidbody m_rigidbody;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        m_isGrounded = false;
    }

    private void OnEnable()
    {
        m_playerInput.actions["Move"].performed += OnMove;
        m_playerInput.actions["Move"].canceled += OnMoveCancel;
        m_playerInput.actions["Jump"].performed += OnJump;
    }

    private void OnDisable()
    {
        m_playerInput.actions["Move"].performed -= OnMove;
        m_playerInput.actions["Move"].canceled -= OnMoveCancel;
        m_playerInput.actions["Jump"].performed -= OnJump;
    }

    private void OnMove(InputAction.CallbackContext callback)
    {
        var value = callback.ReadValue<Vector2>();
        m_direction = new Vector3(value.x, 0, value.y);
    }

    private void OnMoveCancel(InputAction.CallbackContext callback)
    {
        m_direction = Vector3.zero;
    }

    void OnJump(InputAction.CallbackContext callback)
    {
        if (m_isGrounded)
        {
            m_rigidbody.AddForce(transform.up * m_jumpPower, ForceMode.Impulse);
            m_isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        // カメラの正面ベクトルを作成
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // カメラの向きを考慮した移動量
        m_veloctiy = cameraForward * m_direction.z + Camera.main.transform.right * m_direction.x;
        m_veloctiy *= m_speed;

        m_veloctiy.y = m_rigidbody.velocity.y;

        m_rigidbody.velocity = m_veloctiy;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = false;
        }
    }
}
