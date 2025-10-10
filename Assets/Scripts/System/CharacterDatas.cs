using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterDatas")]
public class CharacterDatas : ScriptableObject
{
	public int PlayerOneCharacterId;
	public int PlayerTwoCharacterId; 

	public List<CharacterData> m_characterInfometions;
}

[System.Serializable]
public class CharacterData
{
	public GameObject m_characterPrefab;
	public GameObject m_titleCharacterPrefab;
	public int m_characterId;
	public string m_chacterName;
	public Sprite m_abilityIcon;
}
