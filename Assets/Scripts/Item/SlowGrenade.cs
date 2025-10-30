using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowGrenade : MonoBehaviour
{
	[SerializeField] GameObject SlowField;

    // Update is called once per frame
    void Update()
    {
        
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
		}
	}
}
