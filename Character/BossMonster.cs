using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : BaseMonster
{
    BaseSkill m_skill;
    public BaseSkill GetSkill { get { return m_skill; } }
    public void SetSkill<T>() where T : BaseSkill
    {
        m_skill = gameObject.AddComponent<T>().Init(this);
    }
    public override BaseCharacter Init(int groupHandle, float agentDensity, Stat baseStat, BaseState idle, BaseState attack, BaseState move, BaseState death, BaseState chase = null)
    {
        base.Init(groupHandle, agentDensity, baseStat, idle, attack, move, death, chase);

        m_skill = GetComponent<BaseSkill>().Init(this);
        return this;
    }
}
