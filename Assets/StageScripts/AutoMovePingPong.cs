using UnityEngine;

public class AutoMovePingPong : MonoBehaviour
{
	[SerializeField] float speed = 2f;
	[SerializeField] float moveDistance = 3f;
	private Vector3 startPos;

	void Start()
	{
		startPos = transform.position;
	}

	void Update()
	{
		float x = Mathf.PingPong(Time.time * speed, moveDistance * 2) - moveDistance;
		transform.position = startPos + new Vector3(x, 0, 0);
	}
}
