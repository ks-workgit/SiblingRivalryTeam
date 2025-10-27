using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Slider m_healthBar, m_staminaBar;
    CharacterManager m_characterManager;
    PlayerController m_playerController;

    private void Start()
    {

        //m_healthBar.value = m_characterManager.GetHelth();
        //m_healthBar.maxValue = m_characterManager.GetMaxHealth();
        //m_staminaBar.value = m_characterManager.GetStamina();
        //m_staminaBar.maxValue = m_characterManager.GetMaxStamina();
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
        m_healthBar.value = m_characterManager.GetHelth() / m_characterManager.GetMaxHealth();
        m_staminaBar.value = m_playerController.GetStamina() / m_characterManager.GetMaxStamina();

       // Debug.Log($"stamina: {m_playerController.GetStamina()} / {m_characterManager.GetMaxStamina()} = {m_playerController.GetStamina() / m_characterManager.GetMaxStamina()}");
        //Debug.Log("health:" + m_healthBar.value + "Helth" + m_characterManager.GetHelth() + "MaxHealth" + m_characterManager.GetMaxHealth());
    }
}
