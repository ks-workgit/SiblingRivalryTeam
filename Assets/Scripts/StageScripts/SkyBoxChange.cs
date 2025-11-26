using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxChange : MonoBehaviour
{
    [SerializeField] Material[] m_skyBoxMaterial;

    private void Start()
    {
        if (m_skyBoxMaterial != null)
        {
            // ステージに対応するSkyBoxを設定
            RenderSettings.skybox = m_skyBoxMaterial[MapSelectUI.SelectedMapIndex];
        }
    }
}
