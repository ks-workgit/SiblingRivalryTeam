using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
	[SerializeField] int m_playerNumber;

	[SerializeField] Image m_abilityIcon;

	[SerializeField] GameObject m_player;
	[SerializeField] TextMeshProUGUI m_characterName;

	[SerializeField] CharacterDatas m_characterDatas;

	[SerializeField] MoveGameScene m_moveGameScene;

	[SerializeField] Image m_readyImage;

	int m_characterId;
	int m_characterIdLength;

	bool m_change;
	bool m_isReady;

    void Start()
	{
        TitleChangeCharacter();

        m_characterIdLength = m_characterDatas.m_characterInfometions.Count - 1;
	}

	// スティックを右に倒したとき
	public void RightButtonOnClick()
	{
		if (!m_isReady)
		{
			m_characterId++;
			m_change = true;
        }
	}

    // スティックを左に倒したとき
    public void LeftButtonOnClick()
	{
		if (!m_isReady)
		{
			m_characterId--;
			m_change = true;
        }
	}

	// Readyボタン押したら呼ばれる
	public void ReadyOnclick()
	{
		if(!m_isReady)
		{
			// Readyカウントを増やす
			m_moveGameScene.Ready();

			m_isReady = true;
			m_readyImage.enabled = true;
			m_readyImage.GetComponent<Image>().color = Color.blue;
		}
	}

	private void Update()
	{
		//IDの数値が規定量より大きくなったり小さくなった時にそれ以上いかないように
		if (m_characterId < 0)
		{
			m_characterId = m_characterIdLength;
		}
		else if (m_characterId > m_characterIdLength)
		{
			m_characterId = 0;
		}

		if(m_change)
		{
			TitleChangeCharacter();
		}

		if (m_playerNumber == 1)
		{
			m_characterDatas.PlayerOneCharacterId = m_characterId;
		}
		else
		{
			m_characterDatas.PlayerTwoCharacterId = m_characterId;
		}

		if (!m_isReady)
		{
            m_readyImage.GetComponent<Image>().color = Color.white;
        }
	}

	//キャラクターが変更されたときの見た目の変化など
	void TitleChangeCharacter()
	{
		Destroy(m_player.transform.GetChild(0).gameObject);

		Instantiate(
			m_characterDatas.m_characterInfometions[m_characterId].m_titleCharacterPrefab,
			m_player.transform);

		//アビリティアイコン
		m_abilityIcon.sprite = m_characterDatas.m_characterInfometions[m_characterId].m_abilityIcon;

		//キャラクターの名前
		m_characterName.text = m_characterDatas.m_characterInfometions[m_characterId].m_chacterName;

		m_change = false;
	}

	public bool GetReady()
	{
		return m_isReady;
	}

	public void SetIsReady(bool isReady)
	{
		m_isReady = isReady;
	}
}
