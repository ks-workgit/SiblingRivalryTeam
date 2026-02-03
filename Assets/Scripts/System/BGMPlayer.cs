using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMPlayer : MonoBehaviour
{
	[SerializeField] GameObject m_bgmPrefab;

	bool DontDestroyEnabled = true;

	GameObject m_bgm;

	GameObject m_gameObject;

	// Use this for initialization
	void Start()
	{
		m_gameObject = GameObject.FindWithTag("BGMPlayer");

		if (m_gameObject != null)
		{
			if (m_gameObject != gameObject)
			{
				Destroy(m_gameObject);
			}
		}

		if (DontDestroyEnabled)
		{
			DontDestroyOnLoad(this);
		}

		m_bgm = Instantiate(m_bgmPrefab, transform);
	}

	// Update is called once per frame
	void Update()
	{
		if(m_bgm == null && SceneManager.GetActiveScene().name == "RuleSettings")
		{
			m_bgm = Instantiate(m_bgmPrefab, transform);
		}
		else if(SceneManager.GetActiveScene().name == "GameScene")
		{
			Destroy(m_bgm);
		}

	}
}
