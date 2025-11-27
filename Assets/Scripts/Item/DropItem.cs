using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
	[SerializeField] int m_itemId;

	bool m_isGround;
	bool m_isTouch;

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

		if (m_isTouch)
		{
			m_isTouch = false;
		}
	}

	public int GetItemId()
	{
		return m_itemId;
	}

    public bool GetIsTouch()
    {
        return m_isTouch;
    }

    private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			m_isGround = true;
		}

		if (other.CompareTag("Player"))
		{
            m_isTouch = true;
        }
	}
}
