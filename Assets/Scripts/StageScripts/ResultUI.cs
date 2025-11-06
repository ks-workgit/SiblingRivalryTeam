using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
	[SerializeField] private TimeManager timeManager;
	[SerializeField] private TextMeshPro resultText;
	// Start is called before the first frame update

	private void OnEnable()
	{
		resultText.text = "クリアタイム：" + timeManager.TimeCount.ToString("F2") + "秒";
	}
}
