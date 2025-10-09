using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialHide : MonoBehaviour
{
	[SerializeField] GameObject[] blocks;
	[SerializeField] float waitTime = 10f;

	private int currentIndex = -1;

	void Start()
	{
		StartCoroutine(HideLoop());
	}

	IEnumerator HideLoop()
	{
		while (true)
		{
			// 前のブロックを戻す
			if (currentIndex != -1 && blocks[currentIndex] != null)
				blocks[currentIndex].SetActive(true);

			// 次のブロックへ（最後まで行ったら0に戻る）
			currentIndex = (currentIndex + 1) % blocks.Length;

			// 次のブロックを消す
			if (blocks[currentIndex] != null)
				blocks[currentIndex].SetActive(false);

			yield return new WaitForSeconds(waitTime);
		}
	}
}
