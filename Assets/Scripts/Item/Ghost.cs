using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
	[SerializeField] CharacterDatas m_characterDatas;

	TakeItem m_takeItem;

	int m_usePlayerId;
	int m_enemyPlayerId;

	public void SetUsePlayerId(int usePlayerId)
	{
		m_usePlayerId = usePlayerId;
	}
	
	public void SetTakeItem(TakeItem takeItem)
	{
		m_takeItem = takeItem;
	}
	
	public void StealCrown()
	{
		if(m_usePlayerId == 0)
		{
			m_enemyPlayerId = 1;
		}
		else
		{
			m_enemyPlayerId = 0;
		}

		if (m_characterDatas.CrownCount[m_enemyPlayerId] > 0)
		{
			m_characterDatas.CrownCount[m_usePlayerId]++;
			m_characterDatas.CrownCount[m_enemyPlayerId]--;

			m_takeItem.SetHaveItem(false);
		}
		
		Destroy(gameObject);
	}
}
