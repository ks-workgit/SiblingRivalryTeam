using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TakeWeapon : MonoBehaviour
{
	const int AttackSpeedInitial = 1;
	const float OffSet = 1;	

	[SerializeField] Transform m_handPos;
	[SerializeField] WeaponDatas m_weaponDatas;

	[SerializeField] CharacterManager m_characterManager;
	[SerializeField] PlayerController m_playerController;

	GameObject m_haveWaepon;

	int m_weaponId;
	int m_weaponKind;

	bool m_isHaveWaepon;
	bool m_isDroping;
	bool m_takeDrop;

	public int GetHaveWeaponId()
	{
		return m_weaponId;
	}

	public bool GetTakeDrop()
	{
		return m_takeDrop;
	}

	public void ResetTakeDrop()
	{
		m_takeDrop = false;
	}

	public int GetWeaponKind()
	{
		return m_weaponKind;
	}

	public bool GetIsHaveWeapon()
	{
		return m_isHaveWaepon;
	}

	public void SetIsHaveWeapon(bool isHaveWeapon)
	{
		m_isHaveWaepon = isHaveWeapon;
	}

	private void Start()
	{
		m_weaponId = 0;
	}

	private void Update()
	{
		if(m_playerController.GetWeaPonDrop())
		{
			DropingWeapon(true);
		}

		if(!m_isHaveWaepon || m_isDroping)
		{
			m_playerController.ResetWeaponDrop();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		//武器に触れたら拾う
		if(other.CompareTag("Weapon") && !m_isHaveWaepon && !m_isDroping)
		{
			DropWeapon dropWaepon = other.GetComponent<DropWeapon>();

			GettingWeapon(dropWaepon.GetWeaponId());

			Destroy(other.gameObject);
		}
	}

	public void GettingWeapon(int weaponId)
	{
		m_weaponId = weaponId;

		if(m_isHaveWaepon)
		{
			Destroy(m_haveWaepon.gameObject);
		}

		if(m_weaponId != 0)
		{
			m_haveWaepon = Instantiate(
				m_weaponDatas.m_weaponDatas[m_weaponId].m_weaponPrefabs,
				m_handPos.position,
				Quaternion.identity,
				m_handPos);

			m_haveWaepon.transform.localPosition = m_weaponDatas.m_weaponDatas[m_weaponId].m_weaponPrefabs.transform.position;
			m_haveWaepon.transform.localRotation = m_weaponDatas.m_weaponDatas[m_weaponId].m_weaponPrefabs.transform.rotation;

			Weapon weapon = m_haveWaepon.GetComponent<Weapon>();

			weapon.SetOwner(gameObject);
		}
		else if(m_weaponId == 0 && m_isHaveWaepon)
		{
			//Destroy(m_haveWaepon.gameObject);
		}

		m_characterManager.ResetAttack();

		m_characterManager.GetSetAtttackDamage += m_weaponDatas.m_weaponDatas[m_weaponId].m_attackDamage;
		m_characterManager.GetSetAtttackSpeed = m_weaponDatas.m_weaponDatas[m_weaponId].m_attackSpeed;

		m_weaponKind = m_weaponDatas.m_weaponDatas[m_weaponId].m_weaponKindId;

		m_isHaveWaepon = true;
		m_takeDrop = true;
	}

	//武器を捨てる処理
	public void DropingWeapon(bool dropingWeapon)
	{
		if (m_isHaveWaepon && !m_isDroping)
		{
			if(dropingWeapon)
			{
				//プレイヤーの前方の位置を計算
				Vector3 playerFrontPos = new Vector3(
					transform.position.x + transform.forward.x,
					transform.position.y + transform.forward.y + OffSet,
					transform.position.z + transform.forward.z);

				GameObject dropWeapon = Instantiate(
					m_weaponDatas.m_weaponDatas[m_weaponId].m_dropWeaponPrefabs,
					 playerFrontPos,
					 m_weaponDatas.m_weaponDatas[m_weaponId].m_dropWeaponPrefabs.transform.rotation
					);

				Rigidbody weaponRb = dropWeapon.GetComponent<Rigidbody>();
				//落とした武器を前に投げる
				weaponRb.velocity = new Vector3(transform.forward.x * 7, transform.forward.y * 7, transform.forward.z * 7);
			}

			m_isDroping = true;

			Destroy(m_haveWaepon.gameObject);

			m_playerController.ResetWeaponDrop();

			m_characterManager.ResetAttack();

			m_weaponId = 0;
			m_weaponKind = 0;

			m_isHaveWaepon = false;
			m_takeDrop = true;

			StartCoroutine(ResetHaveWeapon());
		}
	}

	//投げた後すぐに拾えてしまうから時間を開けてから拾えるようにするため
	IEnumerator ResetHaveWeapon()
	{
		yield return new WaitForSeconds(1);

		m_isDroping = false;
	}
}
