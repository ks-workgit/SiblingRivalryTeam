using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamera : MonoBehaviour
{
	[SerializeField] GameObject cameraGuide;

	private float cameraGuideX = 0.0f;    //球のX座標取得
	private float cameraY = 0.0f;  //カメラのY座標取得
	private float cameraZ = 0.0f;  //カメラのZ座標取得
	private float offsetX = 0.0f;  //キャラとカメラのX座標オフセット


	void Start()
	{
		//カメラとボールのX軸差をオフセットとして取り込む
		offsetX = transform.position.x - cameraGuide.transform.position.x;

		//Y・Z軸は操作しないので初期位置を取得しておく
		cameraY = transform.position.y;
		cameraZ = transform.position.z;

		//初期のボールX軸を取得する
		cameraGuideX = cameraGuide.transform.position.x;
	}

	void Update()
	{
		//ボールが右移動してる時だけボールX座標を取得する
		if (cameraGuideX < cameraGuide.transform.position.x)
			cameraGuideX = cameraGuide.transform.position.x;

		//ボールとのオフセットを計算して座標移動させる
		transform.position = new Vector3(cameraGuideX + offsetX, cameraY, cameraZ);
	}
}
