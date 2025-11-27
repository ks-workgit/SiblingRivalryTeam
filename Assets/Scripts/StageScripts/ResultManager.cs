using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
	[Header("UI")]
	[SerializeField] private Image loserIcon; // 負けアイコンを表示
	[SerializeField] private Transform winnerParent; // 勝者を表示する位置

	[Header("通常キャラ")]
	[SerializeField] private GameObject[] characterPrefabs;
	// ※Inspectorでキャラのプレハブを配列にセット

	[SerializeField] private CharacterDatas characterDatas;

	public void ShowResult(bool isPlayerOneWin)
	{
		int winnerId = isPlayerOneWin ?
			characterDatas.PlayerOneCharacterId :
			characterDatas.PlayerTwoCharacterId;

		int loserId = isPlayerOneWin ?
			characterDatas.PlayerTwoCharacterId :
			characterDatas.PlayerOneCharacterId;


		// ◆勝者キャラを生成して配置
		GameObject winObj = Instantiate(
			characterPrefabs[winnerId],
			winnerParent.position,
			Quaternion.identity,
			winnerParent
		);
		winObj.transform.localScale = Vector3.one;

		// ◆勝者キャラのアニメ再生依頼
		Animator anim = winObj.GetComponent<Animator>();
		if (anim != null)
		{
			anim.SetTrigger("Win"); // プレイヤー側で管理する勝利モーション
		}

		// ◆敗者アイコンをセット
		loserIcon.sprite =
			characterDatas.m_characterInfometions[loserId].m_characterIcon;
	}
}
