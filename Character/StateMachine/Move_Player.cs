using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Player : BaseState
{
    public Move_Player(BaseCharacter target) : base(target)
    {

    }
    public override void OnStateEnter()
    {

    }
    public override void OnStateStay(float deltaTime)
    {
        if(m_targetCharacter.GetAStarAgent.State == AStarAgent.EAgentState.Standby)
        {
            m_targetCharacter.State = ECharacterState.Idle;
            return;
        }
        FlockingGroup group = CharacterMng.Instance.GetGroup(m_targetCharacter.GetFlockingAgent.GroupHandle);
        for (int i = 0; i < group.Members.Count; ++i)
        {
            if (group.Members[i] == group.GetOwner)
                continue;

            group.Members[i].MoveAxis = m_targetCharacter.MoveAxis;
            group.Members[i].State = ECharacterState.Move;
        }
        Vector3 lookDirection = m_targetCharacter.transform.localScale;
        lookDirection.x = -1 * Mathf.Sign(m_targetCharacter.GetAStarAgent.Velocity.x);
        m_targetCharacter.transform.localScale = lookDirection;
    }
    public override void OnStateExit()
    {
        m_targetCharacter.GetAStarAgent.Stop();
    }
}
