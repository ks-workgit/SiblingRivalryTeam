using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] Vector3 m_offset = new Vector3(0f, 0.1f, 0f);
    [SerializeField] LayerMask m_groundMask;

    private Vector3 m_direction, m_position;

    [SerializeField] float m_distance;

    public bool CheckGround()
    {
        // ‘«Œ³‚©‚ç‰º‚ÉRay‚ð”ò‚Î‚·
        m_direction = Vector3.down;

        m_position = transform.position + m_offset;

        Ray[] ray = {
            new Ray(m_position, m_direction),
            new Ray(m_position + new Vector3(-0.25f, 0, 0), m_direction),
            new Ray(m_position + new Vector3( 0.25f, 0, 0), m_direction),
            new Ray(m_position + new Vector3(0, 0, -0.25f), m_direction),
            new Ray(m_position + new Vector3(0, 0,  0.25f), m_direction),
        };

        foreach (Ray r in ray)
        {
            Debug.DrawRay(r.origin, r.direction * m_distance, Color.red);
            if (Physics.Raycast(r, m_distance, m_groundMask))
            {
                return true;
            }
        }

        return false;
    }
}
