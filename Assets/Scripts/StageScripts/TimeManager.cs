using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	[SerializeField] private TextMeshPro timetext;
	[SerializeField] private bool isCounting = false;
	
	public float TimeCount {  get; private set; }

    // Update is called once per frame
    void Update()
    {
		if(isCounting)
		{
			TimeCount = Time.deltaTime;
			timetext.text = TimeCount.ToString("F2");
		}
    }

	public void StartCount()
	{
		isCounting = true;
		TimeCount = 0;
	}

	public void StopCount()
	{
		isCounting = false;
	}
}
