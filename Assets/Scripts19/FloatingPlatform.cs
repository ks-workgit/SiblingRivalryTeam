using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
	[SerializeField] private float floatHeight = 3;
	[SerializeField] private float moveSpeed = 2;

	private Vector3 startPos;
	private bool goingUp = true;

    // Start is called before the first frame update
    void Start()
    {
		startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = moveSpeed * Time.deltaTime;
		if (goingUp)
		{
			//毎フレーム移動する距離
			transform.position = Vector3.MoveTowards
				(transform.position,
				startPos + Vector3.up * floatHeight,
				step
				);
			if (Vector3.Distance(transform.position, startPos + Vector3.up * floatHeight) < 0.01f)
				goingUp = false;
		}
		else
		{
			transform.position = Vector3.MoveTowards(
			transform.position,
			startPos,
			step
			);
			if(Vector3.Distance(transform.position,startPos)<0.01f)
				goingUp = true;
		}
    }

	//プレイヤーが乗った時の処理
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			collision.transform.SetParent(transform);
		}
	}
	private void OnCollisionExit(Collision collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			collision.transform.SetParent(null);
		}
	}
}
