using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Enermy : BaseState
{
    public Chase_Enermy(BaseCharacter target) : base(target)
    {

    }
    public override void OnStateEnter()
    {

    }
    public override void OnStateStay(float deltaTime)
    {
        if(m_targetCharacter.Target == null)
        {
            m_targetCharacter.State = ECharacterState.Idle;
            return;
        }

        if(Vector2.Distance(m_targetCharacter.transform.position, m_targetCharacter.Target.transform.position) <= m_targetCharacter.GetStat.AttackRange)
        {
            m_targetCharacter.State = ECharacterState.Attack;
            return;
        }

        Vector3 dirVector = m_targetCharacter.Target.transform.position - m_targetCharacter.transform.position;
        dirVector.Normalize();
        m_targetCharacter.MoveAxis = dirVector;
        m_targetCharacter.transform.position += dirVector * m_targetCharacter.GetStat.MoveSpeed * deltaTime;

        if (m_targetCharacter.Ally == EAllyType.Enermy)
        {
            FlockingGroup group = CharacterMng.Instance.GetGroup(m_targetCharacter.GetFlockingAgent.GroupHandle);
            group.SetOwner = m_targetCharacter;
            for (int i = 0; i < group.Members.Count; ++i)
            {
                if (group.Members[i] == group.GetOwner)
                    continue;

                if (group.Members[i].State != ECharacterState.Idle && group.Members[i].State != ECharacterState.Move)
                    continue;

                group.Members[i].MoveAxis = m_targetCharacter.MoveAxis;
                group.Members[i].State = ECharacterState.Move;
            }
        }
    }
    public override void OnStateExit()
    {

    }
}
