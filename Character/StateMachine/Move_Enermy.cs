using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Enermy : BaseState
{
    public Move_Enermy(BaseCharacter target) : base(target)
    {

    }
    public override void OnStateEnter()
    {

    }
    public override void OnStateStay(float deltaTime)
    {
        FlockingGroup group = CharacterMng.Instance.GetGroup(m_targetCharacter.GetFlockingAgent.GroupHandle);
        if (m_targetCharacter.Ally == EAllyType.Friend)
        {
            if (group.GetOwner.State != ECharacterState.Move)
            {
                m_targetCharacter.State = ECharacterState.Idle;
                return;
            }
            Vector3 dir;
            m_targetCharacter.GetFlockingAgent.GetAxis(group.GetOwner.transform, out dir);
            m_targetCharacter.transform.position += dir * m_targetCharacter.GetStat.MoveSpeed * deltaTime;
        }
        else
        {
            if (m_targetCharacter.Target != null)
            { 
                m_targetCharacter.State = ECharacterState.Chase;
                return;
            }
            Vector3 dir;
            m_targetCharacter.GetFlockingAgent.GetAxis(group.GetOwner.transform, out dir);
            m_targetCharacter.transform.position += dir * m_targetCharacter.GetStat.MoveSpeed * deltaTime;
        }
    }
    public override void OnStateExit()
    {

    }
}
