using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    protected BaseCharacter m_caster;
    protected float m_coolTime;
    float m_elapsedTime;
    public virtual BaseSkill Init(BaseCharacter caster)
    {
        m_caster = caster;
        return this;
    }
    public virtual bool PossibleSkill()
    {
        return m_elapsedTime >= m_coolTime;
    }
    public virtual void UseSkill(out float durationTime)
    {
        durationTime = 0;
        m_elapsedTime = 0;
    }
    void Update()
    {
        if(m_elapsedTime < m_coolTime)
            m_elapsedTime += Time.deltaTime;
    }
}
