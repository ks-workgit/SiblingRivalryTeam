using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCameraGuide : MonoBehaviour
{
	[SerializeField] GameObject CameraGuide;

    // Update is called once per frame
    void Update()
    {
		CameraGuide.transform.position += new Vector3(0.005f,0,0);
    }
}
