using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapstrike : MonoBehaviour
{
	CharacterManager m_characterManager;
	TakeItem m_takeItem;
	TakeWeapon m_playerWeapon;

	[SerializeField] CharacterDatas m_characterDatas;

	int m_playerId;

    // Start is called before the first frame update
    void Start()
    {
        m_playerWeapon = transform.parent.gameObject.GetComponent<TakeWeapon>();
		m_characterManager = transform.parent.gameObject.GetComponent<CharacterManager>();
		m_takeItem = transform.parent.gameObject.GetComponent<TakeItem>();

		m_playerId = m_characterManager.GetPlayerId();
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			//自身を取って来た時無視するように
			if (other.gameObject == transform.parent.gameObject) return;

			//敵のIDを取得
			CharacterManager enemyCharacterManager = other.GetComponent<CharacterManager>();
			int enemyId = enemyCharacterManager.GetPlayerId();

			//武器の交換
			TakeWeapon　enemyWeapon = other.GetComponent<TakeWeapon>();

			if (m_playerWeapon.GetIsHaveWeapon())
			{
				//互いに武器のを持っている場合
				if (enemyWeapon.GetIsHaveWeapon())
				{
					int enemyWeaponId = enemyWeapon.GetHaveWeaponId();

					enemyWeapon.GettingWeapon(m_playerWeapon.GetHaveWeaponId());
					m_playerWeapon.GettingWeapon(enemyWeaponId);
				}
				//相手が武器を持っていない場合
				else
				{
					enemyWeapon.GettingWeapon(m_playerWeapon.GetHaveWeaponId());

					m_playerWeapon.DropingWeapon(false);
				}
			}
			else
			{
				//自分が武器を持っていない場合
				if (enemyWeapon.GetIsHaveWeapon())
				{
					m_playerWeapon.GettingWeapon(enemyWeapon.GetHaveWeaponId());

					enemyWeapon.DropingWeapon(false);
				}
			}

			//王冠の交換
			int enemyCrownCount = m_characterDatas.CrownCount[enemyId];
			m_characterDatas.CrownCount[enemyId] = m_characterDatas.CrownCount[m_playerId];
			m_characterDatas.CrownCount[m_playerId] = enemyCrownCount;

			//アイテムの交換
			TakeItem enemyItem = other.GetComponent<TakeItem>();

			if(m_takeItem.GetHaveItem())
			{
				//互いにアイテムを持っている状態
				if (enemyItem.GetHaveItem())
				{
					int enemyitemId = enemyItem.GetHaveItemId();

					enemyItem.GettingItem(m_takeItem.GetHaveItemId());

					m_takeItem.GettingItem(enemyitemId);
				}
				//相手がアイテムを持っていいない場合
				else
				{
					enemyItem.GettingItem(m_takeItem.GetHaveItemId());

					m_takeItem.SetHaveItem(false);
				}
			}
			else
			{
				//自分がアイテムを持っていない場合
				if (enemyItem.GetHaveItem())
				{
					m_takeItem.GettingItem(enemyItem.GetHaveItemId());

					enemyItem.SetHaveItem(false);
				}
			}

			Destroy(gameObject);
		}
	}
}
