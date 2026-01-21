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
	[SerializeField] Advantageous m_advantageous;

	[SerializeField] Transform m_respawnPos;

    [SerializeField] PlayerUI[] m_playerUI;
    [SerializeField] Slider[] m_healthBar, m_staminaBar;
	[SerializeField] Image[] m_itemIcon;
	[SerializeField] GameObject[] m_abilityIcon;

	TakeItem m_takeItem;

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
	}

	//プレイヤーを生成
	void SpownPlayer(int playerId, Gamepad gamePad)
	{
		int[] characterId = { m_characterDatas.PlayerOneCharacterId, m_characterDatas.PlayerTwoCharacterId };

		m_playerInputManager.playerPrefab =
			m_characterDatas.m_characterInfometions[characterId[playerId]].m_characterPrefab;

		m_playerInputManager.playerPrefab.transform.position = m_respawnPos.position;

		//プレイヤーを生成
		PlayerInput player = m_playerInputManager.JoinPlayer(-1,-1,null, gamePad);

		//代入
		m_takeItem = player.GetComponent<TakeItem>();
		m_takeItem.SetItemIcon(m_itemIcon[playerId]);

		CharacterManager characterManeger = player.GetComponent<CharacterManager>();
		characterManeger.SetPlayerId(playerId);

		m_playerUI[playerId].SetCharacterManager(characterManeger);
		PlayerController playerController = player.GetComponent<PlayerController>();
		m_playerUI[playerId].SetPlayerController(playerController);
        m_playerUI[playerId].SetBar(m_healthBar[playerId], m_staminaBar[playerId]);

        PlayerRespawn playerRespawn = player.GetComponent<PlayerRespawn>();
		playerRespawn.SetRespawnPos(m_respawnPos);

		UseAbility useAbility = player.GetComponent<UseAbility>();
		useAbility.SetAbilityIcon(m_abilityIcon[playerId]);

		m_advantageous.SetCharacterManager(characterManeger);

		Debug.Log("生成　プレイヤーID" + playerId + "キャラクターID" + characterId[playerId]);
	}
}
