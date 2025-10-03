using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_speed;
    [SerializeField] float m_dashSpeed;
    [SerializeField] float m_jumpPower;
    [SerializeField] float m_rollDistance;
    [SerializeField] float m_rollCollTime;

    [SerializeField] Animator m_animator;
    [SerializeField] GroundCheck m_footGround;

    bool m_isGrounded;
    bool m_isDash;
    bool m_isGuard;
    bool m_isAvoidance;

    Vector3 m_direction;
    Vector3 m_velocity;

    PlayerInput m_playerInput;
    Rigidbody m_rigidbody;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        m_isGrounded = true;
        m_isDash = false;
        m_isGuard = false;
        m_isAvoidance = false;
    }

    private void OnEnable()
    {
        m_playerInput.actions["Move"].performed += OnMove;
        m_playerInput.actions["Move"].canceled += OnMoveCancel;
        m_playerInput.actions["Dash"].performed += OnDash;
        m_playerInput.actions["Jump"].performed += OnJump;
        m_playerInput.actions["Attack"].performed += OnAttack;
        m_playerInput.actions["Guard"].performed += OnGuard;
    }

    private void OnDisable()
    {
        m_playerInput.actions["Move"].performed -= OnMove;
        m_playerInput.actions["Move"].canceled -= OnMoveCancel;
        m_playerInput.actions["Dash"].performed -= OnDash;
        m_playerInput.actions["Jump"].performed -= OnJump;
        m_playerInput.actions["Attack"].performed -= OnAttack;
        m_playerInput.actions["Guard"].performed -= OnGuard;
    }

    private void OnMove(InputAction.CallbackContext callback)
    {
        var value = callback.ReadValue<Vector2>();
        var inputDirection = new Vector3(value.x, 0, value.y);

        // ガードしていないときはプレイヤーの移動
        if (!m_isGuard)
        {
            m_direction = inputDirection;
            if (m_isDash) m_animator.SetBool("Dash", true);
            else m_animator.SetBool("Move", true);
            return;
        }

        if (!m_isAvoidance)
        {
            if (inputDirection.sqrMagnitude > 0.01f)
            {
                m_isAvoidance = true;
                StartCoroutine(Avoidance(inputDirection));
            }
        }
    }

    private void OnMoveCancel(InputAction.CallbackContext callback)
    {
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
                Debug.Log("ダッシュ : " +  m_isDash);
                break;
            case InputActionPhase.Canceled:
                // ボタンが離されたとき
                m_isDash = false;
                Debug.Log("ダッシュ : " + m_isDash);
                break;
        }
    }

    private void OnJump(InputAction.CallbackContext callback)
    {
        if (m_isGrounded && !m_isGuard)
        {
            m_rigidbody.AddForce(transform.up * m_jumpPower, ForceMode.Impulse);
            m_isGrounded = false;
        }
    }

    private void OnAttack(InputAction.CallbackContext callback)
    {
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
                m_isGuard = true;
                m_direction = Vector3.zero;
                Debug.Log("防御開始");
                break;
            case InputActionPhase.Canceled:
                // ボタンが離されたとき
                m_isGuard = false;
                Debug.Log("防御終了");
                break;
        }
    }

    private void Update()
    {
        OnGround();
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

        m_velocity.y = m_rigidbody.velocity.y;

        m_rigidbody.velocity = m_velocity;
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

        Debug.Log("接地:" +  m_isGrounded);
    }

    // 回避
    IEnumerator Avoidance(Vector3 direction)
    {
        float rollDuration = 0.2f;
        float elapsed = 0f;
        Vector3 moveDir = direction.normalized;
        float speed = m_rollDistance / rollDuration;

        float checkDistance = speed * Time.deltaTime;

        while (elapsed < rollDuration)
        {
            // Raycastで回避方向をチェック
            if (Physics.Raycast(transform.position, moveDir, checkDistance))
            {
                Debug.Log("壁にぶつかったので回避中断");
                break;
            }

            transform.Translate(moveDir * checkDistance, Space.World);

            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(m_rollCollTime);

        m_isAvoidance = false;
    }
}
