using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
	[SerializeField] int m_itemId;

	bool m_isGround;

	private Rigidbody m_rigidbody;

	private void Start()
	{
		m_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if(m_isGround)
		{
			m_rigidbody.isKinematic = true;
		}
	}

	public int GetItemId()
	{
		return m_itemId;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			m_isGround = true;
		}
	}
}
