using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
	[SerializeField] float speed = 2f;
	[SerializeField] float moveDistance = 3f;
	[SerializeField] Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		float x = Mathf.PingPong(Time.time * speed, moveDistance * 2) - moveDistance;
		transform.position = startPos + new Vector3(x, 0, 0);
    }
}
