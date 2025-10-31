using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCameraGuide : MonoBehaviour
{
	[SerializeField] GameObject CameraGuide;

    // Update is called once per frame
    void FixedUpdate()
    {
		CameraGuide.transform.position += new Vector3(0.05f,0,0);
    }
}
