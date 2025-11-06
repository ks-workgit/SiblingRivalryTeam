using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWeapon : MonoBehaviour
{
	[SerializeField] int m_WeaponId;
	bool m_isGround;

	private Rigidbody m_rigidbody;

	private void Start()
	{
		m_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (m_isGround)
		{
			m_rigidbody.isKinematic = true;
		}
	}

	public int GetWeaponId()
	{
		return m_WeaponId;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			m_isGround = true;
		}
	}
}
