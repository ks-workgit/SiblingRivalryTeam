using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbility : MonoBehaviour
{
	[SerializeField] GameObject[] m_abilityObject;	//アビリティ格納用

	[SerializeField] int m_characterId;

	private void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
		{
            //Use();
        }
    }

	public void Use()
	{
        Instantiate(
                m_abilityObject[m_characterId],
                gameObject.transform.position,
                Quaternion.identity,
                gameObject.transform
                );
    }
}
