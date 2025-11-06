using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeItem : MonoBehaviour
{
	[SerializeField] ItemDatas m_itemDatas;
	[SerializeField] Transform m_playerTransform;
	[SerializeField] CharacterManager m_characterManeger;

	Image m_itemIcon;
	private bool m_nowHaveItem = false;	//今アイテムを持っているか

	int m_haveItemId;	//今持っているアイテムの識別番号は何番かを保存

	HealItem m_healItem;
	Shield m_shield;

	bool m_isDroping;

	public void SetItemIcon(Image image)
	{
		m_itemIcon = image;
	}

	public void SetHaveItem(bool haveItem)
	{
		m_nowHaveItem = haveItem;
	}

	private void Update()
	{
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

		m_itemIcon.sprite = m_itemDatas.m_itemDatas[itemId].m_itemIcon;

		m_haveItemId = itemId;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Item") && !m_nowHaveItem && !m_isDroping)
		{
			//落ちていたアイテムからアイテムの識別番号を貰ってくる
			DropItem dropItem = other.GetComponent<DropItem>();

			int ItemId = dropItem.GetItemId();
			var itemData = m_itemDatas.m_itemDatas[ItemId];

			//アイコン生成
			ObtainingItem(ItemId);

			m_haveItemId = ItemId;

			Debug.Log("拾った");

			//落ちていたアイテムオブジェクトを削除
			Destroy(other.gameObject);
		}

		
	}

	public void ItemUse()
	{
		if (m_nowHaveItem)
		{
            //投げる系アイテム
            if (m_itemDatas.m_itemDatas[m_haveItemId].m_itemKindNum == 0)
            {
				//プレイヤーの前方の位置を計算
				Vector3 playerFrontPos = new Vector3(
					transform.position.x + transform.forward.x,
					transform.position.y + transform.forward.y,
					transform.position.z + transform.forward.z);

				GameObject throwItem = Instantiate(
                    m_itemDatas.m_itemDatas[m_haveItemId].m_itemPrefabs,
				   playerFrontPos,
					Quaternion.identity);

                Rigidbody throwItemRb = throwItem.GetComponent<Rigidbody>();

                throwItemRb.velocity = new Vector3(transform.forward.x * 5, transform.forward.y * 5, transform.forward.z * 5);

				m_nowHaveItem = false;
			}
            //HP回復系アイテム
            else if (m_itemDatas.m_itemDatas[m_haveItemId].m_itemKindNum == 1 &&
                m_characterManeger.GetHelth() < 100)
            {
                GameObject healItem = Instantiate(
                    m_itemDatas.m_itemDatas[m_haveItemId].m_itemPrefabs);

                m_healItem = healItem.GetComponent<HealItem>();

                m_healItem.SetCharacterManeger(m_characterManeger);

                m_healItem.Heal();

				m_nowHaveItem = false;
			}
            //シールド付与系アイテム
            else if (m_itemDatas.m_itemDatas[m_haveItemId].m_itemKindNum == 2 &&
                m_characterManeger.GetShield() < 100)
            {
                GameObject shieldItem = Instantiate(
                    m_itemDatas.m_itemDatas[m_haveItemId].m_itemPrefabs);

                m_shield = shieldItem.GetComponent<Shield>();

                m_shield.SetCharacterManeger(m_characterManeger);

                m_shield.GetShiled();

				m_nowHaveItem = false;
			}
			//王冠を奪うアイテム
			else if(m_itemDatas.m_itemDatas[m_haveItemId].m_itemKindNum == 3)
			{
				GameObject itemGhost = Instantiate(m_itemDatas.m_itemDatas[m_haveItemId].m_itemPrefabs);

				Ghost ghost = itemGhost.GetComponent<Ghost>();

				ghost.SetUsePlayerId(m_characterManeger.GetPlayerId());
				ghost.SetTakeItem(this);

				ghost.StealCrown();
			}

		}
    }

	public void DropItem()
	{
		if(m_nowHaveItem && !m_isDroping)
		{
			//プレイヤーの前方の位置を計算
			Vector3 playerFrontPos = new Vector3(
				transform.position.x + transform.forward.x,
				transform.position.y + transform.forward.y,
				transform.position.z + transform.forward.z);

			GameObject dropItem =  Instantiate(
				m_itemDatas.m_itemDatas[m_haveItemId].m_dropItemPrefabs,
				playerFrontPos,
				Quaternion.identity);

			Rigidbody dropItemRb = dropItem.GetComponent<Rigidbody>();

			dropItemRb.velocity = new Vector3(transform.forward.x * 5, transform.forward.y * 5, transform.forward.z * 5);

			m_nowHaveItem = false;

			m_isDroping = true;

			StartCoroutine(ResetIsDroping());
		}
	}

	IEnumerator ResetIsDroping()
	{
		yield return new WaitForSeconds(1);

		m_isDroping = false;
	}
}
