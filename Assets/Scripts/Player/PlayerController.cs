using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Animator m_animator;
    PlayerInput m_playerInput;
    Rigidbody m_rigidBody;

    [SerializeField] float m_speed;
    [SerializeField] float m_dashSpeed;
    [SerializeField] float m_jumpPower;
    [SerializeField] float m_rollDistance;
    [SerializeField] float m_rollCollTime;
    [SerializeField] GroundCheck m_footGround;

    bool m_isGrounded;
    bool m_isDash;
    bool m_isGuard;
    bool m_isAvoidance;
    bool m_isMoving;
    bool m_canMove;

    Vector3 m_direction;
    Vector3 m_velocity;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_playerInput = GetComponent<PlayerInput>();
        m_rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        m_isGrounded = true;
        m_isDash = false;
        m_isGuard = false;
        m_isAvoidance = false;
        m_isMoving = false;
        m_canMove = true;
    }

    private void OnEnable()
    {
        m_playerInput.actions["Move"].performed += OnMove;
        m_playerInput.actions["Move"].canceled += OnMoveCancel;
        m_playerInput.actions["Dash"].performed += OnDash;
        m_playerInput.actions["Jump"].performed += OnJump;
        m_playerInput.actions["Attack"].performed += OnAttack;
        m_playerInput.actions["Guard"].performed += OnGuard;
        m_playerInput.actions["AvoidanceStick"].performed += OnAvoidanceStick;
        m_playerInput.actions["AvoidanceKey"].performed += OnAvoidanceKey;
    }

    private void OnDisable()
    {
        m_playerInput.actions["Move"].performed -= OnMove;
        m_playerInput.actions["Move"].canceled -= OnMoveCancel;
        m_playerInput.actions["Dash"].performed -= OnDash;
        m_playerInput.actions["Jump"].performed -= OnJump;
        m_playerInput.actions["Attack"].performed -= OnAttack;
        m_playerInput.actions["Guard"].performed -= OnGuard;
        m_playerInput.actions["AvoidanceStick"].performed -= OnAvoidanceStick;
        m_playerInput.actions["AvoidanceKey"].performed -= OnAvoidanceKey;
    }

    private void OnMove(InputAction.CallbackContext callback)
    {
        m_isMoving = true;
        var value = callback.ReadValue<Vector2>();
        m_direction = new Vector3(value.x, 0, value.y);
    }

    private void OnMoveCancel(InputAction.CallbackContext callback)
    {
        m_isMoving = false;
        m_direction = Vector3.zero;
        m_animator.SetBool("Dash", false);
        m_animator.SetBool("Move", false);
    }

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
    {
        if (m_isGrounded && !m_isGuard)
        {
            m_rigidBody.AddForce(transform.up * m_jumpPower, ForceMode.Impulse);
            m_isGrounded = false;
            m_animator.SetTrigger("Jump");            
        }
    }

    private void OnAttack(InputAction.CallbackContext callback)
    {
        if (!m_isGrounded) return;
        m_canMove = false;
        m_rigidBody.velocity = Vector3.zero;
        Debug.Log("攻撃！");
        m_animator.SetTrigger("Attack");
    }

    public void OnGuard(InputAction.CallbackContext callback)
    {
        if (!m_isGrounded) return;

        switch (callback.phase)
        {
            case InputActionPhase.Performed:
                // ボタンが押されたとき
                m_animator.SetBool("Guard", true);
                m_isGuard = true;
                m_direction = Vector3.zero;
                Debug.Log("防御開始");
                break;
            case InputActionPhase.Canceled:
                // ボタンが離されたとき
                m_animator.SetBool("Guard", false);
                m_isGuard = false;
                Debug.Log("防御終了");
                break;
        }
    }

    private void OnAvoidanceStick(InputAction.CallbackContext callback)
    {
        if (!m_isGrounded) return;

        var value = callback.ReadValue<Vector2>();
        var inputDirection = new Vector3(value.x, 0, value.y);
        if (!m_isAvoidance)
        {
            m_isAvoidance = true;
            m_animator.SetTrigger("Roll");
            StartCoroutine(Avoidance(inputDirection));
        }
    }

    private void OnAvoidanceKey(InputAction.CallbackContext callback)
    {
        if (!m_isGrounded) return;

        var forward = transform.forward;
        if (!m_isAvoidance)
        {
            m_isAvoidance = true;
            m_animator.SetTrigger("Roll");                       
            StartCoroutine(Avoidance(forward));            
        }
    }

    public void ResetTrigger()
    {
        m_canMove = true;
        m_animator.ResetTrigger("Jump");
        m_animator.ResetTrigger("Attack");
    }

    private void Update()
    {
        // 移動時のアニメーション
        if (m_isMoving && !m_isGuard)
        {
            if (m_isDash)
            {
                m_animator.SetBool("Dash", true);
            }
            else
            {
                m_animator.SetBool("Dash", false);
                m_animator.SetBool("Move", true);
            }
        }

        OnGround();

        Debug.Log("canMove : " + m_canMove);
    }

    private void FixedUpdate()
    {
        // カメラの正面ベクトルを作成
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // カメラの向きを考慮した移動量
        m_velocity = cameraForward * m_direction.z + Camera.main.transform.right * m_direction.x;
        m_velocity *= m_isDash ? m_dashSpeed : m_speed;
        
        // 進行方向にゆっくり向く
        if (m_velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(m_velocity.normalized), 0.3f);
        }

        // 移動
        if (m_canMove)
        {
            m_velocity.y = m_rigidBody.velocity.y;

            m_rigidBody.velocity = m_velocity;
        }
    }

    // 接地
    private void OnGround()
    {
        if (m_footGround.CheckGround())
        {
            m_isGrounded = true;
        }
        else
        {
            m_isGrounded = false;
            m_isGuard = false;
        }
    }

    // 回避
    IEnumerator Avoidance(Vector3 direction)
    {
        float rollDuration = 0.2f;
        float elapsed = 0f;
        Vector3 moveDir = direction.normalized;
        float speed = m_rollDistance / rollDuration;

        // 回避方向に向く
        transform.rotation = Quaternion.LookRotation(moveDir);

        while (elapsed < rollDuration)
        {
            float delta = Time.deltaTime;
            float checkDistance = speed * delta;

            // Raycastで回避方向をチェック
            if (Physics.Raycast(transform.position, moveDir, checkDistance))
            {
                Debug.Log("壁にぶつかったので回避中断");
                break;
            }

            transform.Translate(moveDir * checkDistance, Space.World);

            elapsed += delta;
            yield return null;
        }

        yield return new WaitForSeconds(m_rollCollTime);

        m_isAvoidance = false;
        m_isGuard = false;
    }
}
