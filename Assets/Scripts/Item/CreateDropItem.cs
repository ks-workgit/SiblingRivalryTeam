using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDropItem : MonoBehaviour
{
	[SerializeField] ItemDatas m_itemDatas;

	[SerializeField] Transform[] CrownSpownPos;

	private void Start()
	{
		SpownItem();
	}

	void SpownItem()
	{
		int spownIndex = Random.Range(0, CrownSpownPos.Length);
		int itemIndex = Random.Range(0, m_itemDatas.m_itemDatas.Count);

		Instantiate(
			m_itemDatas.m_itemDatas[0].m_dropItemPrefabs,
			CrownSpownPos[spownIndex].position,
			Quaternion.identity
			);
	}
}
