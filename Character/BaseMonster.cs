using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : BaseCharacter
{
    public override BaseCharacter Init(int groupHandle, float agentDensity, Stat baseStat, BaseState idle, BaseState attack, BaseState move, BaseState death, BaseState chase = null)
    {
        base.Init(groupHandle, agentDensity, baseStat, idle, attack, move, death, chase);

        m_stateMethodDic.Add(ECharacterState.Chase, chase);

        return this;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        BaseCharacter character = collision.GetComponent<BaseCharacter>();
        if (!character)
            return;
        if (m_targetList.Contains(character))
            return;
        if (character.State == ECharacterState.Death)
            return;
        if (Ally != character.Ally)
        {
            m_targetList.Add(character);
            if (Target == null)
            {
                Target = character;
                State = ECharacterState.Chase;
            }
        }
    }
    public void Kill()
    {
        Destroy(gameObject);
    }
    public void Taming()
    {
        CharacterMng.Instance.MainCharacter.SetTaming = this;
        CharacterMng.Instance.AddGroupMember(CharacterMng.Instance.MainCharacter.GetFlockingAgent.GroupHandle, this);
        Ally = EAllyType.Friend;
        m_stat.ResetHP();
        m_stateMethodDic[ECharacterState.Death].OnStateExit();
        m_characterState = ECharacterState.Idle;
        m_stateMethodDic[ECharacterState.Idle].OnStateEnter();
        m_animator.Play("Idle");
    }
}
