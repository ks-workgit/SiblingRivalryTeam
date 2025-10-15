using UnityEngine;

public class AutoMove : MonoBehaviour
{
	[SerializeField] float speed = 2f;        // ˆÚ“®‘¬“x
	[SerializeField] float moveDistance = 3f; // ˆÚ“®‹——£

	private Vector3 startPos;
	private bool movingRight = true;

	void Start()
	{
		startPos = transform.position;
	}

	void Update()
	{
		// ‰E•ûŒü‚ÉˆÚ“®
		if (movingRight)
		{
			transform.position += Vector3.right * speed * Time.deltaTime;

			// ˆê’è‹——£ˆÚ“®‚µ‚½‚ç”½“]
			if (transform.position.x > startPos.x + moveDistance)
			{
				movingRight = false;
			}
		}
		else // ¶•ûŒü‚ÉˆÚ“®
		{
			transform.position += Vector3.left * speed * Time.deltaTime;

			if (transform.position.x < startPos.x - moveDistance)
			{
				movingRight = true;
			}
		}
	}
}
