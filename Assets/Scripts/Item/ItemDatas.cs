using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemDatas")]
public class ItemDatas : ScriptableObject
{
	public List<ItemInfomertions> m_itemDatas;
}

[System.Serializable]
public class ItemInfomertions
{
	public GameObject m_itemPrefabs;
	public int m_itemId;
	public string m_name;
	public int m_itemKindNum;
}