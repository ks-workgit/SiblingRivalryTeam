using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCrown : MonoBehaviour
{
	[SerializeField] GameObject CrownPrefab;

	[SerializeField] Transform[] CrownSpownPos;

	private void Start()
	{
		SpownCrown();
	}

	void SpownCrown()
	{
		int spownIndex = Random.Range(0, CrownSpownPos.Length);

		Instantiate(
			CrownPrefab,
			CrownSpownPos[spownIndex].position,
			Quaternion.Euler(-90,0,0)
			);
	}
}
