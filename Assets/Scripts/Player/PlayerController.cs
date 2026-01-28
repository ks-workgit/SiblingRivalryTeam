using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    const float StanDuration = 1.5f;    // 被弾したときのスタンする時間
    const float InvincibleTime = 2.0f;  // 被弾した後の無敵時間

	const float Speed = 7;
	const float DashSpeed = 10;
    const float GravityPower = 10.0f;

    const float StaminaRecoveryTime = 1.0f; // スタミナが回復するまでの時間

	[SerializeField] float m_speed = Speed;
    [SerializeField] float m_dashSpeed = 10;
    [SerializeField] float m_jumpSpeed;
    [SerializeField] float m_gravity;
    [SerializeField] float m_fallSpeed;
    [SerializeField] float m_initFallSpeed;
    [SerializeField] float m_rollDistance;
    [SerializeField] float m_rollCollTime;
    [SerializeField] float m_rollStamina = 5;
    [SerializeField] float m_staminaDecrease;
    [SerializeField] float m_staminaRecovery;

    Animator m_animator;
    PlayerInput m_playerInput;
    CharacterController m_characterController;

    [SerializeField] GroundCheck m_footGround;
    [SerializeField] Transform m_shieldPosition;
    [SerializeField] GameObject m_shieldObject;
	[SerializeField] GameObject m_icePrefab;
    Collider m_collider;

    UseAbility m_useAbility;
    CharacterManager m_characterManager;
    TakeItem m_takeItem;
	TakeWeapon m_takeWeapon;
    GameObject m_shieldGenerate;
	GameObject m_iceObject;

    float m_verticalVelocity;
    float m_recoverTime;    // スタンからの復帰時間
    float m_invincibleTimer;    // 被弾後の無敵時間

	float m_speedMagnification;		//足の速さの倍率

	float m_stamina;    // 現在のスタミナ
    float m_duration;   // スタミナが回復するまでの時間

	float m_meltTime;	//氷が解けるまでの時間

    bool m_isGrounded;
    bool m_isDash;
    bool m_isAttacking;
    bool m_isGuard;
    bool m_isAvoidance;
    bool m_isMoving;
    bool m_canMove;
    bool m_isStun;
	bool m_isFreeze;
    bool m_isInvincible;
	bool m_isJump;
	bool m_weaponDrop;

    Vector3 m_direction;
    Vector3 m_velocity;

    private void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        m_playerInput = GetComponent<PlayerInput>();
        m_useAbility = GetComponent<UseAbility>();
        m_characterManager = GetComponent<CharacterManager>();
        m_takeItem = GetComponent<TakeItem>();
		m_takeWeapon = GetComponent<TakeWeapon>();
	}

    private void Start()
    {
        m_isGrounded = true;
        m_isDash = false;
        m_isAttacking = false;
        m_isGuard = false;
        m_isAvoidance = false;
        m_isMoving = false;
        m_canMove = true;
        m_isStun = false;
		m_isFreeze = false;
        m_isInvincible = false;

        m_stamina = m_characterManager.GetStamina();

		m_speedMagnification = 1;		
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
        m_playerInput.actions["Ability"].performed += OnAbility;
        m_playerInput.actions["Item"].performed += OnItem;
		m_playerInput.actions["WeaponDrop"].performed += OnWeaponDrop;
		m_playerInput.actions["ItemDrop"].performed += OnItemDrop;
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
        m_playerInput.actions["Ability"].performed -= OnAbility;
        m_playerInput.actions["Item"].performed -= OnItem;
		m_playerInput.actions["WeaponDrop"].performed -= OnWeaponDrop;
		m_playerInput.actions["ItemDrop"].performed -= OnItemDrop;
	}

    private void OnMove(InputAction.CallbackContext callback)
    {
		if(m_isFreeze)
		{
			m_meltTime -= 0.1f;
		}

        if (m_isStun || m_isGuard || m_isFreeze) return;

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
        if (!m_isGrounded || m_isGuard || m_isStun || m_isAvoidance || m_isAttacking || m_isFreeze) return;

        m_verticalVelocity = m_jumpSpeed;
        m_isGrounded = false;
        m_animator.SetTrigger("Jump");
        m_animator.SetBool("IsGrounded", false);
        m_isJump = true;
	}

    private void OnAttack(InputAction.CallbackContext callback)
    {
        if (!m_isGrounded || m_isGuard || m_isStun || m_isAvoidance || m_isAttacking || m_isFreeze) return;

        m_animator.SetFloat("AttackSpeed", m_characterManager.GetSetAtttackSpeed);
		m_animator.SetInteger("AttackKind", m_takeWeapon.GetWeaponKind());
        m_animator.SetTrigger("Attack");
    }

    public void OnGuard(InputAction.CallbackContext callback)
    {
        if (!m_isGrounded || m_isStun || m_isAvoidance || m_isAttacking || m_isFreeze) return;

        switch (callback.phase)
        {
            case InputActionPhase.Performed:
                if (m_isGuard) return;
                // ボタンが押されたとき
                m_isGuard = true;
                Guard(m_isGuard);
                break;
            case InputActionPhase.Canceled:
                // ボタンが離されたとき
                m_isGuard = false;
                Guard(m_isGuard);
                break;
        }
    }

    private void OnAvoidanceStick(InputAction.CallbackContext callback)
    {
        if (!m_isGrounded || m_isGuard || m_isStun || m_isAttacking || m_stamina <= m_rollStamina || m_isFreeze) return;

        // スティックを倒した方向に回避
        var value = callback.ReadValue<Vector2>();
        var inputDirection = new Vector3(value.x, 0, value.y);
        if (!m_isAvoidance)
        {
            m_isAvoidance = true;
            m_stamina -= m_rollStamina;
            m_animator.SetTrigger("Roll");
            StartCoroutine(Avoidance(inputDirection));
        }
    }

    private void OnAvoidanceKey(InputAction.CallbackContext callback)
    {
        if (!m_isGrounded || m_isGuard || m_isStun || m_isAttacking || m_stamina <= m_rollStamina || m_isFreeze) return;

        // 前方向に回避
        var forward = transform.forward;
        if (!m_isAvoidance)
        {
            m_isAvoidance = true;
            m_stamina -= m_rollStamina;
            m_animator.SetTrigger("Roll");                       
            StartCoroutine(Avoidance(forward));            
        }
    }

    private void OnAbility(InputAction.CallbackContext callback)
    {
        if (m_isStun || m_isAttacking || m_isFreeze) return;
        m_useAbility.Use();
    }

    private void OnItem(InputAction.CallbackContext callback)
    {
        if (m_isStun || m_isAttacking || m_isFreeze) return;
        m_takeItem.ItemUse();
    }

	private void OnWeaponDrop(InputAction.CallbackContext callback)
    {
        if (m_isStun || m_isAttacking || m_isFreeze) return;
        m_weaponDrop = true;
	}

	private void OnItemDrop(InputAction.CallbackContext callback)
    {
        if (m_isStun || m_isAttacking || m_isFreeze) return;
        m_takeItem.DropItem();
	}

	// アニメーションから呼ばれる
	public void ResetTrigger()
    {
        m_canMove = true;
		m_isAttacking = false;

		m_animator.ResetTrigger("Jump");
        m_animator.ResetTrigger("Attack");
    }

    // 攻撃開始
    public void AttackStart()
    {
        m_isAttacking = true;
    }

    // 攻撃終了
    public void AttackEnd()
    {
        m_isAttacking = false;
    }

    // コライダーをオンにする
    public void EnableCollision()
    {
        m_collider.enabled = true;
    }

    // コライダーをオフにする
    public void DisableCollision()
    {
        m_collider.enabled = false;
    }

    // 被弾フラグをセットする
    public void SetIsStun(bool isStun)
    {
        m_recoverTime = StanDuration;
        m_invincibleTimer = InvincibleTime;
        m_isStun = isStun;
        m_isInvincible = true;
        m_isAttacking = false;
        m_animator.SetTrigger("Stun");
    }

	//凍る時の処理
	public void SetIsFreeze(float meltsTime)
	{
		m_isFreeze = true;

        m_canMove = false;

		if(m_iceObject == null)
		{
			m_iceObject = Instantiate(m_icePrefab,transform.position,Quaternion.identity,transform);
			m_iceObject.transform.localPosition = m_icePrefab.transform.position;
		}
		m_animator.speed = 0;

		m_meltTime = meltsTime;
	}

    // 無敵フラグを返す
    public bool GetIsInvincible()
    {
        return m_isInvincible;
    }

    public float GetStamina()
    {
        return m_stamina;
    }	

	public bool GetWeaPonDrop()
	{
		return m_weaponDrop;
	}

	public void ResetWeaponDrop()
	{
		m_weaponDrop = false;
	}

	public bool GetIsGrounded()
	{
		return m_isGrounded;
	}

	public void SetCollider(Collider collider)
	{
		m_collider = collider;
	}

	public bool GetIsStun()
	{
		return m_isStun;
	}

	public float GetSetSpeedMagnification
	{
		get { return m_speedMagnification; }
		set { m_speedMagnification = value; }
	}

	private void FixedUpdate()
    {
        MoveCharacter();
        ApplyGravity();

        // スタミナ消費
        if (m_isDash && m_isMoving && !m_isGuard)
        {
            m_duration = 0;
            m_stamina -= m_staminaDecrease * Time.deltaTime;
            if (m_stamina <= 0)
            {
                m_isDash = false;
            }
        }
        // スタミナ回復
        else
        {
            if (m_stamina <= m_characterManager.GetMaxStamina() && !m_isGuard)
            {
                m_duration += Time.deltaTime;
                if (m_duration >= StaminaRecoveryTime)
                {
                    m_stamina += m_staminaRecovery * Time.deltaTime;
                }
            }
        }

        // 被弾
        if (m_isStun)
        {
            // スタンからの復帰時間を減らしていく
            m_recoverTime -= Time.deltaTime;
            if (m_recoverTime < 0)
            {
                m_recoverTime = 0;
                m_isStun = false;
            }
        }

        // 被弾時の無敵
        if (m_isInvincible && !m_isAvoidance && !m_isGuard)
        {
            // 無敵時間を減らしていく
            m_invincibleTimer -= Time.deltaTime;
            if (m_invincibleTimer < 0)
            {
                m_invincibleTimer = 0;
                m_isInvincible = false;
            }
        }

        // ガード中、空中のときガード解除
        if (!m_isGrounded && m_isGuard)
        {
            if (m_shieldGenerate != null)
            {
                Destroy(m_shieldGenerate);
                m_shieldGenerate = null;
            }

            m_animator.SetBool("Guard", false);
            m_isInvincible = false;
            m_canMove = true;
            m_isGuard = false;
        }

		if(m_isFreeze)
		{
			m_meltTime -= Time.deltaTime;

			if(m_meltTime <= 0)
			{
				IceMelts();
			}
		}
    }

    // 移動関連
    private void MoveCharacter()
    {
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
        if (m_canMove && !m_isStun)
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
    }

    // 重力、接地判定
    private void ApplyGravity()
    {
        // CharacterControllerまたは足元のレイによる接地判定
        bool isGrounded = m_characterController.isGrounded || m_footGround.CheckGround();

        m_animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded)
        {
            // 地面にいるか落下中かつジャンプ中でない場合
            if (m_verticalVelocity <= 0 && !m_isJump)
            {
                // 着地の瞬間
                m_verticalVelocity = -m_initFallSpeed;
            }

            // ジャンプ中でなければ地面にいる間も重力を適用
            if (!m_isJump)
            {
                m_verticalVelocity -= m_gravity * GravityPower * Time.deltaTime;
            }
        }
        else
        {
            // 空中にいるときの処理
            if (!m_isJump)
            {
                // ジャンプ上昇が終わった後は通常の重力をかけて落下させる
                m_verticalVelocity -= m_gravity * Time.deltaTime;
            }
            else
            {
                // ジャンプ直後は重力をかけないためのフラグ
                m_isJump = false;
            }

            // 落下する速さ以上にならないように補正
            if (m_verticalVelocity < -m_fallSpeed)
            {
                m_verticalVelocity = -m_fallSpeed;
            }

        }

        m_isGrounded = isGrounded;

        // 接地してないかつ下向き速度になったら落下中と判定
        bool isFalling = !isGrounded && m_verticalVelocity < 0;

        m_animator.SetBool("IsFalling", isFalling);
    }

    public void OnIsGrounded()
	{
		m_isGrounded = true;
	}

    // ガード
    private void Guard(bool isGuard)
    {
        if (isGuard)
        {
            m_shieldGenerate = Instantiate(m_shieldObject, m_shieldPosition.position, Quaternion.identity);
            m_shieldGenerate.transform.parent = m_shieldPosition;

            m_animator.SetBool("Guard", true);
            m_isInvincible = true;
            m_canMove = false;
        }
        else
        {
            if (m_shieldGenerate != null)
            {
                Destroy(m_shieldGenerate);
                m_shieldGenerate = null;
            }

            m_animator.SetBool("Guard", false);
            m_isInvincible = false;
            m_canMove = true;
        }
    }

    // 回避
    IEnumerator Avoidance(Vector3 direction)
    {
        m_isGuard = false;
        m_isInvincible = true;
        m_duration = 0;

        float rollDuration = 0.3f;
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

            m_characterController.Move(moveDir * checkDistance);

            elapsed += delta;
            yield return null;
        }

        m_isInvincible = false;

        yield return new WaitForSeconds(m_rollCollTime);

        m_isAvoidance = false;
    }

	//時間がたったら氷が解ける
	public void IceMelts()
	{
		m_isFreeze = false;
		m_animator.speed = 1;
		Destroy(m_iceObject);

        m_canMove = true;
	}

	public void ChangeSpeed()
	{
		m_speed *= m_speedMagnification;
		m_dashSpeed *= m_speedMagnification;
	}

	public void InitializationSpeed()
	{
		m_speed = Speed;
		m_dashSpeed = DashSpeed;
	}
}
