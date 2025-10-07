using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] Vector3 m_offset = new Vector3(0f, 0.1f, 0f);

    private Vector3 m_direction, m_position;

    [SerializeField] float m_distance;

    public bool CheckGround()
    {
        // ‘«Œ³‚©‚ç‰º‚ÉRay‚ð”ò‚Î‚·
        m_direction = Vector3.down;

        m_position = transform.position + m_offset;
        Ray ray = new Ray(m_position, m_direction);

        Debug.DrawRay(m_position, m_direction * m_distance, Color.red);

        return Physics.Raycast(ray, m_distance);
    }
}
