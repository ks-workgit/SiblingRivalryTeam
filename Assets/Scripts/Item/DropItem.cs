using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
	[SerializeField] int m_itemId;

	public int GetItemId()
	{
		return m_itemId;
	}
}
