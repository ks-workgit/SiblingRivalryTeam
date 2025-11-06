using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowGrenade : MonoBehaviour
{
	[SerializeField] GameObject SlowField;

	CapsuleCollider m_collider;

    // Update is called once per frame
    void Start()
    {
        m_collider = GetComponent<CapsuleCollider>();
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			Instantiate(
				SlowField,
				transform.position,
				Quaternion.Euler(-90,0,0)
				);

			Destroy(gameObject);
		}
	}
}
