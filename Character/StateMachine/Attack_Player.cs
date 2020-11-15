using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Player : BaseState
{
    public Attack_Player(BaseCharacter target) : base(target)
    {

    }
    public override void OnStateEnter()
    {

    }
    public override void OnStateStay(float deltaTime)
    {
        if (m_targetCharacter.Target == null || m_targetCharacter.Target.State == ECharacterState.Death)
        {
            m_targetCharacter.State = ECharacterState.Idle;
            return;
        }
        if (!m_targetCharacter.IsPossibleBehavior)
            return;

        if (Vector2.Distance(m_targetCharacter.transform.position, m_targetCharacter.Target.transform.position) > m_targetCharacter.GetStat.AttackRange)
        {
            m_targetCharacter.State = ECharacterState.Idle;
            return;
        }

        m_targetCharacter.SetAnimatiorTrigger = "Attack";
        m_targetCharacter.IsPossibleBehavior = false;
        m_targetCharacter.Target.ReceiveDamage(m_targetCharacter.GetStat.AttackDamage);
    }
    public override void OnStateExit()
    {

    }
}
