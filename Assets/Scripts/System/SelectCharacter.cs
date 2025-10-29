using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
	[SerializeField] int m_playerNumder;

	[SerializeField] Image m_characterIcon;
	[SerializeField] Image m_abilityIcon;

	[SerializeField] GameObject m_player;
	[SerializeField] TextMeshProUGUI m_characterName;

	[SerializeField] CharacterDatas m_characterDatas;

	[SerializeField] MoveGameScene m_moveGameScene;

	[SerializeField] Button[] m_button;
	[SerializeField] Image m_readyImage;

	int m_characterId;
	int m_characterIdLenght = 4;

	bool m_change;
	bool m_isReady;

    void Start()
	{
        TitleChangeCharacter();
		m_readyImage.enabled = false;
	}

	public void RightButtonOnClick()
	{
		if (!m_isReady)
		{
			m_characterId++;
			m_change = true;
        }
	}

	public void LeftButtonOnClick()
	{
		if (!m_isReady)
		{
			m_characterId--;
			m_change = true;
        }
	}

	public void ReadyOnclick()
	{
		if(!m_isReady)
		{
			m_moveGameScene.Ready();

			m_isReady = true;
			m_readyImage.enabled = true;
		}
		else
		{
			m_moveGameScene.NotReady();

			m_isReady = false;
            m_readyImage.enabled = false;
        }
	}

	private void Update()
	{
		//IDの数値が規定量より大きくなったり小さくなった時にそれ以上いかないように
		if (m_characterId < 0)
		{
			m_characterId = m_characterIdLenght;
		}
		else if (m_characterId > m_characterIdLenght)
		{
			m_characterId = 0;
		}

		if(m_change)
		{
			TitleChangeCharacter();
		}

		if (m_playerNumder == 1)
		{
			m_characterDatas.PlayerOneCharacterId = m_characterId;
		}
		else
		{
			m_characterDatas.PlayerTwoCharacterId = m_characterId;
		}
	}

	//キャラクターが変更されたときの見た目の変化など
	void TitleChangeCharacter()
	{
		Destroy(m_player.transform.GetChild(0).gameObject);

		Instantiate(
			m_characterDatas.m_characterInfometions[m_characterId].m_titleCharacterPrefab,
			m_player.transform);

		//キャラクターアイコン
		m_characterIcon.sprite = m_characterDatas.m_characterInfometions[m_characterId].m_characterIcon;

		//アビリティアイコン
		m_abilityIcon.sprite = m_characterDatas.m_characterInfometions[m_characterId].m_abilityIcon;

		//キャラクターの名前
		m_characterName.text = m_characterDatas.m_characterInfometions[m_characterId].m_chacterName;

		m_change = false;
	}
}
