using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] float m_speed = 8;
    [SerializeField] float m_jumpPower = 6;

    bool m_isGrounded;

    Vector3 m_direction;
    Vector3 m_veloctiy;
=======
    [SerializeField] float m_speed;
    [SerializeField] float m_dashSpeed;
    [SerializeField] float m_jumpPower;

    bool m_isGrounded;
    bool m_isDash;

    Vector3 m_direction;
    Vector3 m_velocity;
>>>>>>> origin/main

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
<<<<<<< HEAD
=======
        m_isDash = false;
>>>>>>> origin/main
    }

    private void OnEnable()
    {
        m_playerInput.actions["Move"].performed += OnMove;
        m_playerInput.actions["Move"].canceled += OnMoveCancel;
<<<<<<< HEAD
        m_playerInput.actions["Jump"].performed += OnJump;
=======
        m_playerInput.actions["Dash"].performed += OnDash;
        m_playerInput.actions["Jump"].performed += OnJump;
        m_playerInput.actions["Attack"].performed += OnAttack;
        m_playerInput.actions["Guard"].performed += OnGuard;
>>>>>>> origin/main
    }

    private void OnDisable()
    {
        m_playerInput.actions["Move"].performed -= OnMove;
        m_playerInput.actions["Move"].canceled -= OnMoveCancel;
<<<<<<< HEAD
        m_playerInput.actions["Jump"].performed -= OnJump;
=======
        m_playerInput.actions["Dash"].performed -= OnDash;
        m_playerInput.actions["Jump"].performed -= OnJump;
        m_playerInput.actions["Attack"].performed -= OnAttack;
        m_playerInput.actions["Guard"].performed -= OnGuard;
>>>>>>> origin/main
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

<<<<<<< HEAD
    void OnJump(InputAction.CallbackContext callback)
=======
    public void OnDash(InputAction.CallbackContext callback)
    {
        switch (callback.phase)
        {
            case InputActionPhase.Performed:
                // ボタンが押されたとき
                m_isDash = true;
                break;
            case InputActionPhase.Canceled:
                // ボタンが離されたとき
                m_isDash = false;
                break;
        }
    }

    private void OnJump(InputAction.CallbackContext callback)
>>>>>>> origin/main
    {
        if (m_isGrounded)
        {
            m_rigidbody.AddForce(transform.up * m_jumpPower, ForceMode.Impulse);
            m_isGrounded = false;
        }
    }

<<<<<<< HEAD
=======
    private void OnAttack(InputAction.CallbackContext callback)
    {
        Debug.Log("攻撃！");
    }

    private void OnGuard(InputAction.CallbackContext callback)
    {
        Debug.Log("防御！");
    }

>>>>>>> origin/main
    private void FixedUpdate()
    {
        // カメラの正面ベクトルを作成
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // カメラの向きを考慮した移動量
<<<<<<< HEAD
        m_veloctiy = cameraForward * m_direction.z + Camera.main.transform.right * m_direction.x;
        m_veloctiy *= m_speed;

        m_veloctiy.y = m_rigidbody.velocity.y;

        m_rigidbody.velocity = m_veloctiy;
    }

    private void OnCollisionEnter(Collision collision)
=======
        m_velocity = cameraForward * m_direction.z + Camera.main.transform.right * m_direction.x;
        m_velocity *= m_isDash ? m_dashSpeed : m_speed;
        
        // 進行方向にゆっくり向く
        if (m_velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(m_velocity.normalized), 0.3f);
        }

        m_velocity.y = m_rigidbody.velocity.y;

        m_rigidbody.velocity = m_velocity;
    }

    private void OnCollisionStay(Collision collision)
>>>>>>> origin/main
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = true;
        }
    }
<<<<<<< HEAD
=======

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = false;
        }
    }
>>>>>>> origin/main
}
