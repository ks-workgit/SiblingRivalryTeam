using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownCount : MonoBehaviour
{
	int m_crownCount = 0;

	public int GetCrownCount()
	{
		return m_crownCount;
	}

	private void OnTriggerStay(Collider other)
	{
		//�����ɐG�ꂽ�Ƃ��ɉ����̐��𑝂₷
		if(other.CompareTag("Crown"))
		{
			m_crownCount++;

			Debug.Log("����������");
			Debug.Log("��������" + m_crownCount);
		}
	}

}
