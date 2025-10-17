using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCrown : MonoBehaviour
{
	[SerializeField] GameObject CrownPrefab;

	void CrownCount()
	{
		Instantiate(CrownPrefab);
	}
}
