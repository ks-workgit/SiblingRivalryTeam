using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
	[SerializeField]  float scrollSpeed = 2f;
	[SerializeField]  float resetXPosition = 50f;
	[SerializeField]  Vector3 resetPosition = Vector3.zero;

	private bool shouldReset = false;

	void Update()
	{
		if (!shouldReset)
		{
			// ���t���[���E�Ɉړ�
			transform.position += new Vector3(scrollSpeed * Time.deltaTime, 0f, 0f);

			// ���̈ʒu�ɗ����烊�Z�b�g
			if (transform.position.x >= resetXPosition)
			{
				shouldReset = true;
				transform.position = resetPosition;
			}
		}
	}
}
