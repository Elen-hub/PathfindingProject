using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI_Minimap : MonoBehaviour
{
    const float m_distanceFactor = 10;
    Image m_maskImg;
    RawImage m_fogBuffer;
    Image m_mapImg;
    public MapUI_Minimap Init()
    {
        m_maskImg = transform.Find("Mask").GetComponent<Image>();
        m_fogBuffer = transform.Find("Mask").Find("MinimapBuffer").GetComponent<RawImage>();
        m_mapImg = transform.Find("Mask").Find("Minimap").GetComponent<Image>();
        transform.Find("SizeBTN").GetComponent<Button>().onClick.AddListener(OnMapButton);

        m_fogBuffer.material.SetFloat("_rectSize", m_maskImg.rectTransform.rect.width);
        m_fogBuffer.material.SetFloat("_textureSize", m_fogBuffer.rectTransform.rect.width);
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
    private void LateUpdate()
    {
        m_mapImg.transform.localPosition = CharacterMng.Instance.MainCharacter.transform.position * m_distanceFactor * -1;
        m_fogBuffer.material.SetFloat("_posX", CharacterMng.Instance.MainCharacter.transform.position.x / m_fogBuffer.rectTransform.rect.width * m_distanceFactor);
        m_fogBuffer.material.SetFloat("_posY", CharacterMng.Instance.MainCharacter.transform.position.y / m_fogBuffer.rectTransform.rect.height * m_distanceFactor);
    }
    void OnMapButton()
    {
        UIMng.Instance.Open<MapUI>(UIMng.UIName.MapUI).IsMax = true;
    }
}
