using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Animator m_animator;
    PlayerInput m_playerInput;
    CharacterController m_characterController;

    [SerializeField] float m_speed;
    [SerializeField] float m_dashSpeed;
    [SerializeField] float m_jumpSpeed;
    [SerializeField] float m_gravity;
    [SerializeField] float m_fallSpeed;
    [SerializeField] float m_initFallSpeed;
    [SerializeField] float m_rollDistance;
    [SerializeField] float m_rollCollTime;
    [SerializeField] GroundCheck m_footGround;

    float m_verticalVelocity;

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
        m_characterController = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        m_playerInput = GetComponent<PlayerInput>();
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
        if (!m_isGrounded || m_isGuard) return;

        m_verticalVelocity = m_jumpSpeed;
        m_isGrounded = false;
        m_animator.SetTrigger("Jump");            
    }

    private void OnAttack(InputAction.CallbackContext callback)
    {
        if (!m_isGrounded) return;

        m_canMove = false;
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
                m_canMove = false;
                Debug.Log("防御開始");
                break;
            case InputActionPhase.Canceled:
                // ボタンが離されたとき
                m_animator.SetBool("Guard", false);
                m_isGuard = false;
                m_canMove = true;
                Debug.Log("防御終了");
                break;
        }
    }

    private void OnAvoidanceStick(InputAction.CallbackContext callback)
    {
        if (!m_isGrounded || m_isGuard) return;

        // スティックを倒した方向に回避
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
        if (!m_isGrounded || m_isGuard) return;

        // 前方向に回避
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
        var isGrounded = m_characterController.isGrounded;

        if (isGrounded && !m_isGrounded)
        {
            // 着地する瞬間に落下の初速を指定しておく
            m_verticalVelocity = -m_initFallSpeed;
        }
        else if (!isGrounded)
        {
            // 空中にいるときは下向きに重力加速度を与えて落下させる
            m_verticalVelocity -= m_gravity * Time.deltaTime;

            // 落下する速さ以上にならないように補正
            if (m_verticalVelocity < -m_fallSpeed)
            {
                m_verticalVelocity = -m_fallSpeed;
            }
        }

        m_isGrounded = isGrounded;

        // カメラの正面ベクトルを作成
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // カメラの向きを考慮した移動量
        m_velocity = cameraForward * m_direction.z + Camera.main.transform.right * m_direction.x;
        m_velocity *= m_isDash ? m_dashSpeed : m_speed;

        var moveVelocity = new Vector3(
            m_velocity.x,
            m_verticalVelocity,
            m_velocity.z
        );

        // 現在フレームの移動量を移動速度から計算
        var moveDelta = moveVelocity * Time.deltaTime;

        // 進行方向にゆっくり向く
        if (m_velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(m_velocity.normalized), 0.3f);
        }

        // 移動
        if (m_canMove)
        {
            m_characterController.Move(moveDelta);
        }

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
        float rollDuration = 0.3f;
        float elapsed = 0f;
        Vector3 moveDir = direction.normalized;
        float speed = m_rollDistance / rollDuration;

        // 回避方向に向く
        transform.rotation = Quaternion.LookRotation(moveDir);

        m_isGuard = false;

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

            m_characterController.Move(moveDir * checkDistance);

            elapsed += delta;
            yield return null;
        }

        yield return new WaitForSeconds(m_rollCollTime);

        m_isAvoidance = false;
    }
}
