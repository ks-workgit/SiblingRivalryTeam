using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenDominion : MonoBehaviour
{
	[SerializeField] GameObject IceStormPrefab;

    // Start is called before the first frame update
    void Start()
    {
		CreateIceStorm();
	}

	void CreateIceStorm()
	{
		 GameObject iceStormObject = Instantiate(
			IceStormPrefab,
			transform.position,
			Quaternion.identity);

		IceStorm iceStorm = iceStormObject.GetComponent<IceStorm>();

		iceStorm.SetUsePlayer(transform.parent.gameObject);

		Destroy(gameObject);
	}
}
