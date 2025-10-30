using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
	const int UiOffSet = 60;

    [SerializeField] Slider m_healthBar, m_staminaBar;
	[SerializeField] CharacterDatas m_characterDatas;

	[SerializeField] GameObject m_remainingLifeUiPrefab;
	[SerializeField] RectTransform m_parent;

	[SerializeField] TextMeshProUGUI m_crownText;

    CharacterManager m_characterManager;
    PlayerController m_playerController;

	List<GameObject> m_remainingLifeUis = new List<GameObject>();

	int m_remainingLife;
	int m_beforeRemainingLife;

	private void Start()
    {
		CreateRemainingLifeUi();
	}

    public void SetBar(Slider health, Slider stamina)
    {
        m_healthBar = health;
        m_staminaBar = stamina;
    }

    public void SetCharacterManager(CharacterManager characterManager)
    {
        m_characterManager = characterManager;
    }

    public void SetPlayerController(PlayerController playerController)
    {
        m_playerController = playerController;
    }

    private void Update()
    {
		m_remainingLife = m_characterManager.GetRemainingLife();

		m_healthBar.value = m_characterManager.GetHelth() / m_characterManager.GetMaxHealth();
        m_staminaBar.value = m_playerController.GetStamina() / m_characterManager.GetMaxStamina();

		DestoryRemainingLifeUi();
		CreateCrownUi();
    }

	//残機のUI生成
	void CreateRemainingLifeUi()
	{
		m_remainingLife = m_characterManager.GetRemainingLife();

		//UIの生成
		if (m_characterManager.GetPlayerId() == 0)
		{
			Image remainingLifeUi = m_remainingLifeUiPrefab.GetComponent<Image>();
			remainingLifeUi.sprite =
				m_characterDatas.m_characterInfometions[m_characterDatas.PlayerOneCharacterId].m_characterIcon;

			for (int i = 0; i < m_remainingLife; i++)
			{
				GameObject remainingLife = Instantiate(
					m_remainingLifeUiPrefab,
					m_parent
					);

				remainingLife.transform.position = new Vector3(
					remainingLife.transform.position.x + (UiOffSet * i), remainingLife.transform.position.y , remainingLife.transform.position.z);

				m_remainingLifeUis.Add(remainingLife);
			}			
		}
		else
		{
			Image remainingLifeUi = m_remainingLifeUiPrefab.GetComponent<Image>();
			remainingLifeUi.sprite =
				m_characterDatas.m_characterInfometions[m_characterDatas.PlayerTwoCharacterId].m_characterIcon;

			for (int i = 0; i < m_remainingLife; i++)
			{
				GameObject remainingLife = Instantiate(
					m_remainingLifeUiPrefab,
					m_parent
					);

				remainingLife.transform.position = new Vector3(
					remainingLife.transform.position.x - (UiOffSet * i), remainingLife.transform.position.y, remainingLife.transform.position.z);

				m_remainingLifeUis.Add (remainingLife);
			}
		}

		//残機数を保存
		m_beforeRemainingLife = m_remainingLife;
	}

	//残機が減ったら削除
	void DestoryRemainingLifeUi()
	{
		if(m_beforeRemainingLife != m_remainingLife)
		{
			Destroy(m_remainingLifeUis[m_remainingLifeUis.Count - 1]);

			m_remainingLifeUis.RemoveAt(m_remainingLifeUis.Count - 1);

			m_beforeRemainingLife = m_remainingLife;
		}
	}

	// 王冠の数を表示
	void CreateCrownUi()
	{
        m_crownText.text = $"× {m_characterDatas.CrownCount[m_characterManager.GetPlayerId()]}";      		
	}
}
