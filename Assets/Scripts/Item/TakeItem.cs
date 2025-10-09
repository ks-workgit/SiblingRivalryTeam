using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeItem : MonoBehaviour
{
	[SerializeField] Image m_itemIcon;
	[SerializeField] ItemDatas m_itemDatas;
	[SerializeField] Transform m_playerTransform;
	[SerializeField] CharacterManeger m_characterManeger;

	private bool m_nowHaveItem = false;	//今アイテムを持っているか

	int m_haveItemId;	//今持っているアイテムの識別番号は何番かを保存

	HealItem m_healItem;
	Shield m_shield;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Q) && m_nowHaveItem)
		{
			ItemUse();
		}

		if(!m_nowHaveItem)
		{
			m_itemIcon.enabled = false;
		}
		else
		{
			m_itemIcon.enabled = true;
		}
	}

	//拾ったアイテムのアイコンを設定
	public void ObtainingItem(int itemId)
	{
		m_nowHaveItem = true;

		m_itemIcon.sprite = LoadItemIcon.Load(itemId);

		m_haveItemId = itemId;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Item") && !m_nowHaveItem)
		{
			//落ちていたアイテムからアイテムの識別番号を貰ってくる
			DropItem dropItem = other.GetComponent<DropItem>();

			int ItemId = dropItem.GetItemId();
			var itemData = m_itemDatas.m_itemDatas[ItemId];

			//アイコン生成
			ObtainingItem(ItemId);

			//拾ったアイテムを生成しておく
			if(itemData.m_itemKindNum != 0)
			{
				//Instantiate(itemData.m_itemPrefabs);
			}

			m_haveItemId = ItemId;

			Debug.Log("拾った");

			//落ちていたアイテムオブジェクトを削除
			Destroy(other.gameObject);
		}
	}

	public void ItemUse()
	{
		//投げる系アイテム
		if (m_itemDatas.m_itemDatas[m_haveItemId].m_itemKindNum == 0)
		{
			GameObject throwItem = Instantiate(
				m_itemDatas.m_itemDatas[m_haveItemId].m_itemPrefabs,
				new Vector3(m_playerTransform.position.x, m_playerTransform.position.y, m_playerTransform.position.z),
				Quaternion.identity);

			Rigidbody throwItemRb = throwItem.GetComponent<Rigidbody>();

			Vector3 forward = transform.forward;

			throwItemRb.velocity = transform.forward;			
		}
		//HP回復系アイテム
		else if(m_itemDatas.m_itemDatas[m_haveItemId].m_itemKindNum == 1 && 
			m_characterManeger.GetHelth() < 100)
		{
			GameObject healItem = Instantiate(
				m_itemDatas.m_itemDatas[m_haveItemId].m_itemPrefabs);

			m_healItem = healItem.GetComponent<HealItem>();

			m_healItem.SetCharacterManeger(m_characterManeger);

			m_healItem.Heal();
		}
		//シールド付与系アイテム
		else if(m_itemDatas.m_itemDatas[m_haveItemId].m_itemKindNum == 2 &&
			m_characterManeger.GetShield() < 100)
		{
			GameObject shieldItem = Instantiate(
				m_itemDatas.m_itemDatas[m_haveItemId].m_itemPrefabs);

			m_shield = shieldItem.GetComponent<Shield>();

			m_shield.SetCharacterManeger(m_characterManeger);

			m_shield.GetShiled();
		}

		m_nowHaveItem = false;
	}
}
