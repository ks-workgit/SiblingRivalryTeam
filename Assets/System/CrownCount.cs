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
		//‰¤Š¥‚ÉG‚ê‚½‚Æ‚«‚É‰¤Š¥‚Ì”‚ğ‘‚â‚·
		if(other.CompareTag("Crown"))
		{
			m_crownCount++;

			Debug.Log("‰¤Š¥‚°‚Á‚Æ");
			Debug.Log("‰¤Š¥‚©‚¸" + m_crownCount);
		}
	}

}
