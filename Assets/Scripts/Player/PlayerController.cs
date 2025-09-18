using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_speed;
    [SerializeField] float m_dashSpeed;
    [SerializeField] float m_jumpPower;

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
        m_isGrounded = false;
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

        if (!m_isGuard)
        {
            m_direction = inputDirection;
            return;
        }

        if (!m_isAvoidance)
        {
            if (inputDirection.sqrMagnitude > 0.01f)
            {
                m_isAvoidance = true;
                StartCoroutine(Avoidance(inputDirection));
                Debug.Log("����I");
            }
        }
    }

    private void OnMoveCancel(InputAction.CallbackContext callback)
    {
        m_direction = Vector3.zero;
        m_isAvoidance = false;
    }

    public void OnDash(InputAction.CallbackContext callback)
    {
        Debug.Log("Dash Phase: " + callback.phase);
        switch (callback.phase)
        {
            case InputActionPhase.Performed:
                // �{�^���������ꂽ�Ƃ�
                m_isDash = true;
                Debug.Log("�_�b�V��" + m_isDash);
                break;
            case InputActionPhase.Canceled:
                // �{�^���������ꂽ�Ƃ�
                m_isDash = false;
                Debug.Log("�_�b�V��" + m_isDash);
                break;
        }
    }

    private void OnJump(InputAction.CallbackContext callback)
    {
        if (m_isGrounded)
        {
            m_rigidbody.AddForce(transform.up * m_jumpPower, ForceMode.Impulse);
            m_isGrounded = false;
        }
    }

    private void OnAttack(InputAction.CallbackContext callback)
    {
        Debug.Log("�U���I");
    }

    public void OnGuard(InputAction.CallbackContext callback)
    {
        Debug.Log("Guard Phase: " + callback.phase);
        switch (callback.phase)
        {
            case InputActionPhase.Performed:
                // �{�^���������ꂽ�Ƃ�
                m_isGuard = true;
                m_direction = Vector3.zero;
                Debug.Log("�h��J�n");
                break;
            case InputActionPhase.Canceled:
                // �{�^���������ꂽ�Ƃ�
                m_isGuard = false;
                Debug.Log("�h��I��");
                break;
        }
    }

    private void FixedUpdate()
    {
        // �J�����̐��ʃx�N�g�����쐬
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �J�����̌������l�������ړ���
        m_velocity = cameraForward * m_direction.z + Camera.main.transform.right * m_direction.x;
        m_velocity *= m_isDash ? m_dashSpeed : m_speed;
        
        // �i�s�����ɂ���������
        if (m_velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(m_velocity.normalized), 0.3f);
        }

        m_velocity.y = m_rigidbody.velocity.y;

        m_rigidbody.velocity = m_velocity;
    }

    private void OnCollisionStay(Collision collision)
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

    IEnumerator Avoidance(Vector3 direction)
    {
        float distance = 3.0f;    // �������
        float duration = 0.2f;  // ����ɂ����鎞��
        float elapsed = 0f;

        Vector3 start = transform.position;
        // ����̍��W���v�Z
        Vector3 target = start + direction.normalized * distance;


        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(start, target, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = target;

        yield return new WaitForSeconds(1.0f);

        m_isAvoidance = false;
    }
}
