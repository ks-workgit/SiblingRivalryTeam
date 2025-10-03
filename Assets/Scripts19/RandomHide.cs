using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHide : MonoBehaviour
{
	[SerializeField] GameObject[] blocks;
	[SerializeField] float waitTime = 10f;

	private int hiddenIndex = -1;

	// Start is called before the first frame update
	void Start()
    {
		StartCoroutine(HideLoop());
	}

	IEnumerator HideLoop()
	{
		while (true)
		{
			// 前回消えたブロックを戻す
			if (hiddenIndex != -1 && blocks[hiddenIndex] != null)
				blocks[hiddenIndex].SetActive(true);

			// ランダムで新しいブロックを選ぶ
			int newIndex = Random.Range(0, blocks.Length);
			while (newIndex == hiddenIndex && blocks.Length > 1)
				newIndex = Random.Range(0, blocks.Length);

			// 選ばれたブロックを消す
			if (blocks[newIndex] != null)
				blocks[newIndex].SetActive(false);

			hiddenIndex = newIndex;

			// 待機
			yield return new WaitForSeconds(waitTime);
		}
	}
}