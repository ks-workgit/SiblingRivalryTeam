using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameObject : MonoBehaviour
{
	[SerializeField] WeaponDatas m_weaponDatas;
	[SerializeField] ItemDatas m_itemDatas;
	[SerializeField] GameObject CrownPrefab;

	[SerializeField] List<Transform> SpownPos = new List<Transform>();

	private void Start()
	{
		GetSpownPos();

		SpownGameObject();
	}

	void GetSpownPos()
	{
		int spownPosCount = this.gameObject.transform.childCount;

		for (int i = 0; i < spownPosCount; i++)
		{
			SpownPos.Add(gameObject.transform.GetChild(i));
		}
	}

	void SpownGameObject()
	{
		int spownIndex = Random.Range(0, SpownPos.Count);
		int itemIndex = Random.Range(0, m_itemDatas.m_itemDatas.Count);
		int weaponIndex = Random.Range(1, m_weaponDatas.m_weaponDatas.Count);

		//‰¤Š¥‚Ì¶¬
		Instantiate(
			CrownPrefab,
			SpownPos[spownIndex].position,
			Quaternion.Euler(-90, 0, 0)
			);

		SpownPos.RemoveAt(spownIndex);

		spownIndex = Random.Range(0, SpownPos.Count);

		//ƒAƒCƒeƒ€‚Ì¶¬
		Instantiate(
			m_itemDatas.m_itemDatas[itemIndex].m_dropItemPrefabs,
			SpownPos[spownIndex].position,
			Quaternion.identity
			);

		SpownPos.RemoveAt(spownIndex);

		spownIndex = Random.Range(0, SpownPos.Count);

		//•Ší‚Ì¶¬
		Instantiate(
			m_weaponDatas.m_weaponDatas[weaponIndex].m_dropWeaponPrefabs,
			SpownPos[spownIndex].position,
			Quaternion.Euler(-90, 0, 0)
			);
	}
}
