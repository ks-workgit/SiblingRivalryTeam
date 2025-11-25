using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
	[Header("キャラデータ")]
	[SerializeField] CharacterDatas m_characterDatas;

	[Header("UI")]
	[SerializeField] Image m_winnerIcon;
	[SerializeField] Image m_loserIcon;

	GameObject winnerObj;
	GameObject loserObj;

	public void SetWinner(int winner, Transform winnerPos,Transform loserPos)
	{
		UpdateResuItUI(winner);
		SpawnCharacters(winner,winnerPos,loserPos);
	}
	public void UpdateResuItUI(int winner)
	{
		int p1id = m_characterDatas.PlayerOneCharacterId;
		int p2id = m_characterDatas.PlayerTwoCharacterId;

		if(winner == 1)
		{
			m_winnerIcon.sprite = m_characterDatas.m_characterInfometions[p1id].m_characterIcon;
			m_loserIcon.sprite = m_characterDatas.m_characterInfometions[p2id].m_characterIcon;
		}
		else 
		{
			m_winnerIcon.sprite = m_characterDatas.m_characterInfometions[p2id].m_characterIcon;
			m_loserIcon.sprite = m_characterDatas.m_characterInfometions[p1id].m_characterIcon;
		}
	}
	public void SpawnCharacters(int winner,Transform winnerPos,Transform loserPos)
	{
		int p1id = m_characterDatas.PlayerOneCharacterId;
		int p2id = m_characterDatas.PlayerTwoCharacterId;

		int winId = (winner == 1) ? p1id : p2id;
		//winnerObj = Instantiate(m_characterDatas.m_characterInfometions[winId].m_titleCharacterPrefab, winnerPos);
		int loseId = (winner == 1) ?p2id : p1id;
		//loserObj = Instantiate(m_characterDatas.m_characterInfometions[loseId].m_titleCharacterPrefab, loserPos);
	}
}
