using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : UIBase
{
    StringBuilder m_hpTextString;
    BaseCharacter m_character;
    Text m_text;
    Image m_img;
    public override UIBase Init()
    {
        base.Init();
        m_img = transform.Find("HP").Find("Progress").GetComponent<Image>();
        m_text = transform.Find("HP").Find("Text").GetComponent<Text>();
        return this;
    }
    public override void Open()
    {
        m_character = CharacterMng.Instance.MainCharacter;
        gameObject.SetActive(true);
    }
    public override void Close()
    {
        gameObject.SetActive(false);
    }
    void LateUpdate()
    {
        m_hpTextString = new StringBuilder(m_character.CurrHP.ToString("F0"));
        m_hpTextString.Append(" / ");
        m_hpTextString.Append(m_character.GetStat.HP.ToString("F0"));
        m_text.text = m_hpTextString.ToString();
        m_img.fillAmount = m_character.HPPercent;
    }
}
