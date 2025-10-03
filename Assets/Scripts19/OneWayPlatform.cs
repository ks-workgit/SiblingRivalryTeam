using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
	private Collider platformCollider;

	// Start is called before the first frame update
	void Start()
    {
        platformCollider = GetComponent<Collider>();
    }
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Rigidbody rb = collision.rigidbody;

			// プレイヤーが下から突き上げてきた場合（上向き速度がある場合）
			if (rb != null && rb.velocity.y > 0)
			{
				// 一時的に当たり判定を無効化
				Physics.IgnoreCollision(collision.collider, platformCollider, true);
			}
		}
	}
	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			// 離れたら再び当たり判定を有効化
			Physics.IgnoreCollision(collision.collider, platformCollider, false);
		}
	}
}
