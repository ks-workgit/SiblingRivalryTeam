using UnityEngine;
using System.Collections.Generic;

public class MapSpawner : MonoBehaviour
{
	//ゲームシーン側
	[SerializeField] private MapGroup[] mapGroups;
	[SerializeField] private float mapWidth = 90f;
	[SerializeField] private bool inTitle;

	private List<GameObject> activeMaps = new List<GameObject>();
	private float spawnX = 0f;
	private int selectedIndex;

	[System.Serializable]
	public class MapGroup
	{
		public GameObject[] mapVariants;
		public Vector3 spawnOffset;
	}

	void Start()
	{
		selectedIndex = MapSelectUI.SelectedMapIndex;

		if (selectedIndex < 0 || selectedIndex >= mapGroups.Length)
		{
			Debug.LogError("マップ選択が無効です。");
			return;
		}

		if(inTitle)
		{
			selectedIndex = Random.Range(0, mapGroups.Length);
		}

		// 最初に4枚並べる
		for (int i = 0; i < 4; i++)
		{
			int variant = i % mapGroups[selectedIndex].mapVariants.Length;
			SpawnMap(selectedIndex, variant);
		}
	}

	void Update()
	{
		if (activeMaps.Count == 0) return;

		GameObject firstMap = activeMaps[0];
		if (firstMap.transform.position.x + mapWidth < Camera.main.transform.position.x - 30f)
		{
			Destroy(firstMap);
			activeMaps.RemoveAt(0);

			int nextVariant = Random.Range(0, mapGroups[selectedIndex].mapVariants.Length);
			SpawnMap(selectedIndex, nextVariant);
		}
	}

	void SpawnMap(int mapIndex, int variantIndex)
	{
		GameObject prefab = mapGroups[mapIndex].mapVariants[variantIndex];
		Vector3 offset = mapGroups[mapIndex].spawnOffset;
		GameObject map = Instantiate(prefab, new Vector3(spawnX, 0, 0) + offset, Quaternion.identity);
		activeMaps.Add(map);
		spawnX += mapWidth;
	}
}
