using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCameraGuide : MonoBehaviour
{
	const float MoveSpeed = 0.05f;

	[SerializeField] GameObject CameraGuide;
	[SerializeField] CharacterDatas m_characterDatas;

	float m_moveSpeed = MoveSpeed;

	// Update is called once per frame
	void FixedUpdate()
    {
		for(int i = 0; i <= m_characterDatas.CrownCount.Length - 1; i++)
		{
			if (m_characterDatas.CrownCount[i] >= 3)
			{
				m_moveSpeed = MoveSpeed * 1.15f;
			}
			else if(m_characterDatas.CrownCount[i] >= 5)
			{
				m_moveSpeed = MoveSpeed * 1.3f;
			}
			else if (m_characterDatas.CrownCount[i] >= 8)
			{
				m_moveSpeed = MoveSpeed * 1.4f;
			}

		}

		CameraGuide.transform.position += new Vector3(m_moveSpeed, 0,0);
    }
}
