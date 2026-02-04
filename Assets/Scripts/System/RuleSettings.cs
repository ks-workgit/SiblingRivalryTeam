using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RuleSettings : MonoBehaviour
{
    [SerializeField] RuleSettingsData m_ruleSettingsData;
    [SerializeField] TextMeshProUGUI m_roundText, m_lifeText, m_crownText;
    [SerializeField] Button[] m_buttons;

	[SerializeField] AudioSource m_buttonSe;
	[SerializeField] AudioClip m_switchingSe;

    GameObject m_lastSelected;

    bool m_roundSelect;
    bool m_lifeSelect;
    bool m_crownSelect;
	bool m_onClick;

    public enum Setting
    {
        Round,
        Life,
        Crown,
    }

    private void Start()
    {
        m_roundText.text = m_ruleSettingsData.RoundsCount[m_ruleSettingsData.m_roundCount].ToString();
        m_lifeText.text = m_ruleSettingsData.LifeCount[m_ruleSettingsData.m_lifeCount].ToString();
        m_crownText.text = m_ruleSettingsData.VictoryCrownCount[m_ruleSettingsData.m_crownCount].ToString();
    }

    private void Update()
    {
        // 現在選択中のオブジェクトを取得
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

        if (selectedObject != null)
        {
            for (int i = 0; i < m_buttons.Length; i++)
            {
                if (selectedObject == m_buttons[i].gameObject)
                {
                    Setting setting = (Setting)i;
                    SelectedObject(setting);
                }
            }

            if (selectedObject != m_lastSelected)
            {
                m_buttonSe.PlayOneShot(m_switchingSe);
                m_lastSelected = selectedObject;
            }
        }

		if(m_onClick)
		{
			if(!m_buttonSe.isPlaying)
			{
				SceneManager.LoadScene("Stagechoice");
			}
		}
    }

    private void SelectedObject(Setting setting)
    {
        // どれが選択中かを更新
        m_roundSelect = setting == Setting.Round;
        m_lifeSelect = setting == Setting.Life;
        m_crownSelect = setting == Setting.Crown;

        // 選択された項目に応じて処理
        switch (setting)
        {
            case Setting.Round:
                UpdateSetting(ref m_ruleSettingsData.m_roundCount, m_ruleSettingsData.RoundsCount, m_roundText);
                break;
            case Setting.Life:
                UpdateSetting(ref m_ruleSettingsData.m_lifeCount, m_ruleSettingsData.LifeCount, m_lifeText);
                break;
            case Setting.Crown:
                UpdateSetting(ref m_ruleSettingsData.m_crownCount, m_ruleSettingsData.VictoryCrownCount, m_crownText);
                break;
        }
    }

    // UIを反映させる
    private void UpdateSetting(ref int count, int[] values, TextMeshProUGUI text)
    {
        if (count >= values.Length) count = 0;
        if (count < 0) count = values.Length - 1;

        text.text = values[count].ToString();
    }

    // 配列用のカウントを増やす
    public void SetRoundCount(int count)
    {
        m_ruleSettingsData.m_roundCount += count;
    }
    public void SetLifeCount(int count)
    {
        m_ruleSettingsData.m_lifeCount += count;
    }
    public void SetCrownCount(int count)
    {
        m_ruleSettingsData.m_crownCount += count;
    }

    // 現在の選択状態を返す
    public bool GetRoundSelect()
    {
        return m_roundSelect;
    }
    public bool GetLifeSelect()
    {
        return m_lifeSelect;
    }
    public bool GetCrownSelect()
    {
        return m_crownSelect;
    }

    // シーン遷移するボタン
    public void OnClickButton()
    {
		m_buttonSe.Play();

		m_onClick = true;
    }
}
