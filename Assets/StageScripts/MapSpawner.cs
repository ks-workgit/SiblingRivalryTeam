using UnityEngine;
using System.Collections.Generic;
public class MapSpawner : MonoBehaviour
{
	[SerializeField] private GameObject[] mapPrefabs; // 4つのマップ
	[SerializeField] private float mapWidth = 50f;    // マップの幅

	private List<GameObject> activeMaps = new List<GameObject>();
	private int nextMapIndex = 0; // 次に出すマップの番号
	private float spawnX = 0f;    // 次に生成する位置

	void Start()
	{
		// 最初に4枚並べる
		for (int i = 0; i < mapPrefabs.Length; i++)
		{
			SpawnMap(i);
		}
	}

	void Update()
	{
		// 左側のマップが画面外に出たら削除して次を生成
		if (activeMaps.Count > 0)
		{
			GameObject firstMap = activeMaps[0];
			if (firstMap.transform.position.x + mapWidth < Camera.main.transform.position.x - 30f)
			{
				// 1番左のマップ削除
				Destroy(firstMap);
				activeMaps.RemoveAt(0);

				// 次のマップ生成（ループ）
				nextMapIndex = (nextMapIndex + 1) % mapPrefabs.Length;
				SpawnMap(nextMapIndex);
			}
		}
	}

	void SpawnMap(int prefabIndex)
	{
		GameObject prefab = mapPrefabs[prefabIndex];
		GameObject map = Instantiate(prefab, new Vector3(spawnX, 0, 0), prefab.transform.rotation);
		activeMaps.Add(map);
		spawnX += mapWidth;
	}
}