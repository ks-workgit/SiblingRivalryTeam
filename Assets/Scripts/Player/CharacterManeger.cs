using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManeger : MonoBehaviour
{
	const int MaxHelth = 100;
	static int[] RemainingLife = { 3, 5 };

	int m_helth = MaxHelth;
	int m_crownCount = 0;
	int m_remainingLife;

	bool m_isDeth = false;

	public int GetCrownCount()
	{
		return m_crownCount;
	}

	public bool GetIsDeth()
	{
		return m_isDeth;
	}

	// Start is called before the first frame update
	void Start()
    {
		m_remainingLife = RemainingLife[0];
	}

    // Update is called once per frame
    void Update()
    {
        if(m_helth <= 0)
		{
			KnockDown();
		}

		if(m_remainingLife <= 0)
		{
			m_isDeth = true;
		}
    }

	void KnockDown()
	{
		m_remainingLife--;

		m_helth = MaxHelth;
	}

	//�_���[�W����
	public void Damage(int damage)
	{
		m_helth -= damage;
	}

	private void OnTriggerEnter(Collider other)
	{
		//�����ɐG�ꂽ�Ƃ��ɉ����̐��𑝂₷
		if (other.CompareTag("Crown"))
		{
			m_crownCount++;

			Debug.Log("����������");
			Debug.Log("��������" + m_crownCount);
		}
	}
}
