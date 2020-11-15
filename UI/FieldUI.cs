using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldUI : UIBase
{
    List<TamingKillUI> m_instanceList = new List<TamingKillUI>();
    public override UIBase Init()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
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

    public TamingKillUI SetTamingUI(BaseMonster monster)
    {
        for(int i =0; i<m_instanceList.Count; ++i)
        {
            if(!m_instanceList[i].gameObject.activeSelf)
            {
                m_instanceList[i].Enalbed(monster);
                return m_instanceList[i];
            }
        }

        TamingKillUI ui = Instantiate(Resources.Load<TamingKillUI>("UI/Instance/TamingKillUI"), transform).Init();
        ui.Enalbed(monster);
        m_instanceList.Add(ui);
        return ui;
    }
}
