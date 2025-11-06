using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
	[SerializeField] private TimeManager timeManager;
	[SerializeField] private GameObject resultPanel;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("player"))
		{
			timeManager.StopCount();
			resultPanel.SetActive(false);
		}
	}
}
