using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : UIBase
{
    MapUI_Minimap m_miniMap;
    MapUI_MaxMap m_maxMap;
    public bool IsMax {
        set {
            if (value)
            {
                m_miniMap.Disanbled();
                m_maxMap.Enabled();
            }
            else
            {
                m_maxMap.Disanbled();
                m_miniMap.Enabled();
            }
        }
    }
    public override UIBase Init()
    {
        base.Init();
        m_miniMap = GetComponentInChildren<MapUI_Minimap>().Init();
        m_maxMap = GetComponentInChildren<MapUI_MaxMap>().Init();
        m_maxMap.Disanbled();
        return this;
    }
    public override void Open()
    {
        gameObject.SetActive(true);
    }
    public override void Close()
    {
        gameObject.SetActive(false);
    }
}
