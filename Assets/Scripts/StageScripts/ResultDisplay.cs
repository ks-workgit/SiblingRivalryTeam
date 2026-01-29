using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultDisplay : MonoBehaviour
{
	[Header("順位表示")]
	[SerializeField] private List<GameObject> rankedPlayers;

	[Header("位置表示")]
	[SerializeField] private Transform[] rankePositions;

    void Start()
    {
		ShowReesut();
    }

    // Update is called once per frame
    private void ShowReesut()
	{
		for (int i = 0; i < rankedPlayers.Count && i < rankePositions.Length; i++)
		{
			GameObject player = rankedPlayers[i];

			player.transform.position = rankePositions[i].position;
			player.transform.rotation = rankePositions[i].rotation;

			var controller = player.GetComponent<PlayerController>();
			if(controller != null)
				controller.enabled = false;

			var rd = player.GetComponent<Rigidbody>();
			if(rd != null)
			{
				rd.velocity = Vector3.zero;
				rd.isKinematic = true;
			}

			/*
			//アニメーション用
			 var anim = player.GetComponent<Animator>();
			if (anim != null)
				anim.Play("Result");*/
		}
	}
}
