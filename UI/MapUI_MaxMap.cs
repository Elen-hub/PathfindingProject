using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI_MaxMap : MonoBehaviour
{
    const float m_distanceFactor = 10;
    Image m_maskImg;
    RawImage m_fogBuffer;
    Image m_mapImg;

    public MapUI_MaxMap Init()
    {
        m_maskImg = transform.Find("Mask").GetComponent<Image>();
        m_fogBuffer = transform.Find("Mask").Find("MinimapBuffer").GetComponent<RawImage>();
        m_mapImg = transform.Find("Mask").Find("Minimap").GetComponent<Image>();
        transform.Find("SizeBTN").GetComponent<Button>().onClick.AddListener(OnMapButton);
        m_fogBuffer.material.SetFloat("_rectSize", 0);
        m_fogBuffer.material.SetFloat("_textureSize", 0);
        return this;
    }
    public void Enabled()
    {
        gameObject.SetActive(true);
    }
    public void Disanbled()
    {
        gameObject.SetActive(false);
    }
    void OnMapButton()
    {
        UIMng.Instance.Open<MapUI>(UIMng.UIName.MapUI).IsMax = false;
    }
}
