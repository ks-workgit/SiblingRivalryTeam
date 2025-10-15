using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.GridLayoutGroup;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject m_owner;
    [SerializeField] int m_damage;

    Collider m_collider;

    [SerializeField] CharacterManeger m_characterManeger;

    private void Start()
    {
        m_collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 自分自身なら無視
        if (other.transform.root == m_owner.transform) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log($"{m_owner.name} hit {other.name}");

            // PlayerControllerを持つオブジェクトが当たったら
            if (other.TryGetComponent(out PlayerController playerController))
            {
                // そのオブジェクトの動きを止める
                playerController.SetIsStun(true);
                //playerController.OnHit();
                m_characterManeger.Damage(m_damage);
                Debug.Log("ヒット");
            }

            // 多段ヒット防止
            m_collider.enabled = false;
        }
    }
}
