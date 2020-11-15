using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Player : BaseState
{
    public Idle_Player(BaseCharacter target) : base(target)
    {

    }
    public override void OnStateEnter()
    {

    }
    public override void OnStateStay(float deltaTime)
    {
        if (m_targetCharacter.Target == null)
        {
            m_targetCharacter.FindTarget();
            return;
        }

        float testAttackDistance = 2.5f;
        if (Vector2.Distance(m_targetCharacter.transform.position, m_targetCharacter.Target.transform.position) <= testAttackDistance)
        {
            m_targetCharacter.State = ECharacterState.Attack;
            return;
        }
    }
    public override void OnStateExit()
    {

    }
}
