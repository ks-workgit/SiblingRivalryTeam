using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapSelectUI : MonoBehaviour
{
	[Header("UI参照 ")]
	[SerializeField] private Image mapImage;          // 現在選択中のマップ画像
	[SerializeField] private Button prevButton;       // 「前へ」ボタン
	[SerializeField] private Button nextButton;       // 「次へ」ボタン
	[SerializeField] private Button startButton;      // 「スタート」ボタン

	[Header("マップ画像")]
	[SerializeField] private Sprite[] mapSprites;     // UIで表示するマップ画像たち

	public static int SelectedMapIndex = 0;           // 選ばれたマップ番号を他シーンに渡す用
	private int currentIndex = 0;

	void Start()
	{
		// 最初の画像を表示
		UpdateMapDisplay();

		// ボタンイベント設定
		prevButton.onClick.AddListener(PrevMap);
		nextButton.onClick.AddListener(NextMap);
		startButton.onClick.AddListener(StartGame);
	}

	void PrevMap()
	{
		currentIndex--;
		if (currentIndex < 0) currentIndex = mapSprites.Length - 1;
		UpdateMapDisplay();
	}

	void NextMap()
	{
		currentIndex++;
		if (currentIndex >= mapSprites.Length) currentIndex = 0;
		UpdateMapDisplay();
	}

	void UpdateMapDisplay()
	{
		mapImage.sprite = mapSprites[currentIndex];
		SelectedMapIndex = currentIndex;
	}

	void StartGame()
	{
		Debug.Log("選ばれたマップ番号: " + SelectedMapIndex);
		SceneManager.LoadScene("Stage");
	}
}
