using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour
{
    public static RuleManager Instance { get; private set; }

    public static RuleSettingsData CurrentRule;

    [SerializeField] private RuleSettingsData m_ruleSettingsData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (CurrentRule == null) CurrentRule = m_ruleSettingsData;
        }
        else
        {
            Destroy(gameObject); // ‚·‚Å‚É‘¶İ‚·‚éê‡‚Íd•¡‚ğ”jŠü
        }
    }
}
