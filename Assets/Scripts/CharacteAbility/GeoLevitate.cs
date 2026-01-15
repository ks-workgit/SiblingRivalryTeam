using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoLevitate : MonoBehaviour
{
	const int ThrowSpeed = 1000;	//投げる速さ
	const int MaxRockCount = 4;

	[SerializeField] GameObject m_rockPrefab;
	[SerializeField] GameObject[] m_levitateRock;

	UseAbility m_useAbility;
	GameObject m_rock;
	GameObject m_player;

	int m_rockCount = MaxRockCount;

	public void SetUseAblity(UseAbility useAbility)
	{
		m_useAbility = useAbility;
	}

	// Start is called before the first frame update
	void Start()
    {
		m_player = transform.parent.gameObject;

		m_useAbility = m_player.GetComponent<UseAbility>();
	}

    // Update is called once per frame
    void Update()
	{
		//スキル使用後再度△を押したら岩を投げる
		if (m_useAbility.GetReactivation() && m_rockCount >= 0)
		{
			ThrowRock();

			m_useAbility.ResetReactivation();
		}
    }

	void ThrowRock()
	{
		Vector3 playerFrontPos = new Vector3(
					transform.position.x + transform.forward.x,
					transform.position.y + transform.forward.y,
					transform.position.z + transform.forward.z);
		//生成する位置が低すぎるのでy座標を上げている
		Vector3 thorwPos = new Vector3(playerFrontPos.x, playerFrontPos.y + 1.5f, playerFrontPos.z);

		//岩を生成
		m_rock = Instantiate(
			m_rockPrefab,
			thorwPos,
			Quaternion.identity
			);

		Rock rock = m_rock.GetComponent<Rock>();

		rock.SetPlayer(m_player);
		rock.SetGeoLevitate(this);

		//投げる力を加える
		Rigidbody rockRigidbody = m_rock.GetComponent<Rigidbody>();
		rockRigidbody.AddForce(m_player.transform.forward * ThrowSpeed);

		//投げてすぐにプレイヤーと干渉するのを防ぐ処理
		Collider collider = m_rock.GetComponent<Collider>();
		StartCoroutine(OnCollider(collider));

		m_levitateRock[m_rockCount].SetActive(false);

		m_rockCount--;
	}

	IEnumerator OnCollider(Collider collider)
	{
		yield return new WaitForSeconds(0.05f);

		collider.enabled = true;

		yield return new WaitForSeconds(1);

		if (m_rockCount < 0)
		{
			Destroy(gameObject);
		}
	}

	public void RockHit()
	{
		for (int i = 0; i <= MaxRockCount; i++)
		{
			m_levitateRock[i].SetActive(true);
		}

		m_rockCount = MaxRockCount;
	}
}
