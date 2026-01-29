using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultDisplay : MonoBehaviour
{
	[Header("èáà ï\é¶")]
	[SerializeField] private List<GameObject> rankedPlayers;

	[Header("à íuï\é¶")]
	[SerializeField] private Transform[] rankePositions;

	[SerializeField] CharacterDatas m_characterDatas;

	bool m_createFinished = false;

    void Start()
    {
		ShowReesut();
    }

    // Update is called once per frame
    private void ShowReesut()
	{
		for (int i = 0; i < rankedPlayers.Count && i < rankePositions.Length; i++)
		{
			int winPlayerId = RoundCount.m_winPlayerId;
			GameObject player = null;

			if(!m_createFinished)
			{
				if (winPlayerId == 0)
				{
					player =Instantiate(
						m_characterDatas.m_characterInfometions[m_characterDatas.PlayerOneCharacterId].m_titleCharacterPrefab,
						rankePositions[i].position,
						rankePositions[i].rotation
						);
					m_createFinished = true;
				}
				else
				{
					player = Instantiate(
						m_characterDatas.m_characterInfometions[m_characterDatas.PlayerTwoCharacterId].m_titleCharacterPrefab,
						rankePositions[i].position,
						rankePositions[i].rotation
						);
					m_createFinished = true;
				}
			}
			else
			{
				if (winPlayerId != 0)
				{
					player = Instantiate(
						m_characterDatas.m_characterInfometions[m_characterDatas.PlayerOneCharacterId].m_titleCharacterPrefab,
						rankePositions[i].position,
						rankePositions[i].rotation
						);
				}
				else
				{
					player = Instantiate(
						m_characterDatas.m_characterInfometions[m_characterDatas.PlayerTwoCharacterId].m_titleCharacterPrefab,
						rankePositions[i].position,
						rankePositions[i].rotation
						);
				}
			}

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
		}
	}
}
