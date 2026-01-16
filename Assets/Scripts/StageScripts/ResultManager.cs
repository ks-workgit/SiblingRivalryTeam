using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
	[Header("キャラクター情報")]
	[SerializeField] private CharacterDatas m_characterDatas;

	[Header("親Transform")]
	[SerializeField] private Transform firstPlayerParent;  // 1位の配置先
	[SerializeField] private Transform secondPlayerParent; // 2位の配置先

	[Header("順位UI")]
	[SerializeField] private Image firstRankImage;
	[SerializeField] private Image secondRankImage;

	private GameObject firstPlayerInstance;
	private GameObject secondPlayerInstance;

	// シーン側で順位画像を渡す
	public void SetRankImages(GameObject firstRankObj, GameObject secondRankObj)
	{
		if (firstRankObj != null)
			firstRankImage = firstRankObj.GetComponent<Image>();
		if (secondRankObj != null)
			secondRankImage = secondRankObj.GetComponent<Image>();

		firstRankImage.enabled = true;
		secondRankImage.enabled = true;
	}

	void Start()
	{
		ShowResult();
	}

	void ShowResult()
	{
		int round1P = RoundCount.Round1P;
		int round2P = RoundCount.Round2P;

		int player1CharacterId = m_characterDatas.PlayerOneCharacterId;
		int player2CharacterId = m_characterDatas.PlayerTwoCharacterId;

		if (round1P > round2P)
		{
			SpawnCharacter(player1CharacterId, firstPlayerParent, ref firstPlayerInstance);
			SpawnCharacter(player2CharacterId, secondPlayerParent, ref secondPlayerInstance);
		}
		else
		{
			SpawnCharacter(player2CharacterId, firstPlayerParent, ref firstPlayerInstance);
			SpawnCharacter(player1CharacterId, secondPlayerParent, ref secondPlayerInstance);
		}
	}

	void SpawnCharacter(int characterId, Transform parent, ref GameObject instance)
	{
		if (characterId < 0 || characterId >= m_characterDatas.m_characterInfometions.Count)
		{
			Debug.LogWarning("ResultManager: キャラクターIDが不正です");
			return;
		}

		if (instance != null)
			Destroy(instance);

		GameObject prefab = m_characterDatas.m_characterInfometions[characterId].m_titleCharacterPrefab;
		if (prefab != null)
		{
			instance = Instantiate(prefab, parent);
			instance.transform.localPosition = Vector3.zero;
			instance.transform.localRotation = Quaternion.identity;
		}
		else
		{
			Debug.LogWarning("ResultManager: キャラクタープレハブが設定されていません");
		}
	}
}
