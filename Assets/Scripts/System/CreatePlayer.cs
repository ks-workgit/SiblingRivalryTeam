using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CreatePlayer : MonoBehaviour
{
	[SerializeField] PlayerInputManager m_playerInputManager;
	[SerializeField] CharacterDatas m_characterDatas;
	[SerializeField] IssueJudgement m_issueJudgement;

	[SerializeField] Transform m_respawnPos;

    [SerializeField] PlayerUI[] m_playerUI;
    [SerializeField] Slider[] m_healthBar, m_staminaBar;
	[SerializeField] Image[] m_itemIcon;
	[SerializeField] GameObject[] m_abilityIcon;

	TakeItem m_takeItem;
	List<Advantageous> m_advantageous = new List<Advantageous>();
	List<CharacterManager> m_characterManager = new List<CharacterManager>();

	int m_createPlayerCount = 0;

	private void Start()
	{
		var devices = InputSystem.devices;

		foreach (var device in devices)
		{
			if (device is Gamepad)
			{
				Gamepad gamepad = device as Gamepad;
				Debug.Log($"コントローラー検出: {gamepad.displayName}");

				SpownPlayer(m_createPlayerCount, gamepad);

				m_createPlayerCount++;
			}
		}

		for(int i = 0; i < m_advantageous.Count; i++)
		{
			m_advantageous[i].SetCharacterManager(m_characterManager);
		}
	}

	//プレイヤーを生成
	void SpownPlayer(int playerId, Gamepad gamePad)
	{
		int[] characterId = { m_characterDatas.PlayerOneCharacterId, m_characterDatas.PlayerTwoCharacterId };

		m_playerInputManager.playerPrefab =
			m_characterDatas.m_characterInfometions[characterId[playerId]].m_characterPrefab;

		//1Pと2Pで位置をずらす
		if(playerId == 0)
		{
			m_playerInputManager.playerPrefab.transform.position = 
				new Vector3(m_respawnPos.position.x, m_respawnPos.position.y,m_respawnPos.position.z + 4);
		}
		else
		{
			m_playerInputManager.playerPrefab.transform.position =
				new Vector3(m_respawnPos.position.x, m_respawnPos.position.y, m_respawnPos.position.z - 2);
		}
		//キャラクターの向きを正面に
		m_playerInputManager.playerPrefab.transform.rotation = Quaternion.Euler(0f,90f,0f);


		//プレイヤーを生成
		PlayerInput player = m_playerInputManager.JoinPlayer(-1, -1, null, gamePad);

		//代入
		m_takeItem = player.GetComponent<TakeItem>();
		m_takeItem.SetItemIcon(m_itemIcon[playerId]);

		CharacterManager characterManeger = player.GetComponent<CharacterManager>();
		m_characterManager.Add(characterManeger);
		characterManeger.SetPlayerId(playerId);

		m_playerUI[playerId].SetCharacterManager(characterManeger);
		PlayerController playerController = player.GetComponent<PlayerController>();
		m_playerUI[playerId].SetPlayerController(playerController);
        m_playerUI[playerId].SetBar(m_healthBar[playerId], m_staminaBar[playerId]);

        PlayerRespawn playerRespawn = player.GetComponent<PlayerRespawn>();
		playerRespawn.SetRespawnPos(m_respawnPos);

		UseAbility useAbility = player.GetComponent<UseAbility>();
		useAbility.SetAbilityIcon(m_abilityIcon[playerId]);

		m_advantageous.Add(player.GetComponent<Advantageous>());

		Debug.Log("生成　プレイヤーID" + playerId + "キャラクターID" + characterId[playerId]);
	}
}
