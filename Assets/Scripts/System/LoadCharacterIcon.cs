using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadCharacterIcon : MonoBehaviour
{
	public static Sprite Load(int ItemId)
	{
		var rawData = File.ReadAllBytes("Assets/Character/CharacterIcons/" + ItemId.ToString("000") + ".png");
		Texture2D texture = new Texture2D(0, 0);
		texture.LoadImage(rawData);
		return Sprite.Create(
			texture,
			new Rect(0f, 0f, texture.width, texture.height),
			new Vector2(0.5f, 0.5f),
			100f);
	}
}
