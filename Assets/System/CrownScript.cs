using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownScript : MonoBehaviour
{
	private void OnTriggerStay(Collider other)
	{
		if(other.CompareTag("Player"))
		{


			Destroy(gameObject);
		}
	}
}
