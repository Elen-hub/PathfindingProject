using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TamingKillUI : MonoBehaviour
{
    Vector3 m_pivot = new Vector2(0, -1.5f);
    BaseMonster m_monster;
    public TamingKillUI Init()
    {
        transform.Find("OnTaming").GetComponent<Button>().onClick.AddListener(() => 
        {
            m_monster.Taming();
            Disabled();
        });
        transform.Find("OnKill").GetComponent<Button>().onClick.AddListener(() =>
        {
            m_monster.Kill();
            Disabled();
        });
        return this;
    }
    public void Enalbed(BaseMonster target)
    {
        m_monster = target;
        gameObject.SetActive(true);
    }
    public void Disabled()
    {
        gameObject.SetActive(false);
    }
    private void LateUpdate()
    {
        transform.position = m_monster.transform.position + m_pivot;
    }
}
