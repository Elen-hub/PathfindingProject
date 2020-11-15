using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Enermy : BaseState
{
    public Idle_Enermy(BaseCharacter target) : base(target)
    {

    }
    public override void OnStateEnter()
    {

    }
    public override void OnStateStay(float deltaTime)
    {
        if(m_targetCharacter.Target != null)
            m_targetCharacter.State = ECharacterState.Chase;
    }
    public override void OnStateExit()
    {

    }
}
