using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Default : BaseState
{
    public Death_Default(BaseCharacter target) : base(target)
    {

    }
    public override void OnStateEnter()
    {
        m_targetCharacter.UsingAgroCollider = false;
        m_targetCharacter.Target = null;
        m_targetCharacter.GetTargetList.Clear();
        CharacterMng.Instance.RemoveGroupMember(m_targetCharacter.GetFlockingAgent.GroupHandle, m_targetCharacter);
        if (m_targetCharacter.Ally == EAllyType.Enermy)
        {
            UIMng.Instance.Open<FieldUI>(UIMng.UIName.FieldUI).SetTamingUI(m_targetCharacter as BaseMonster);
        }
    }
    public override void OnStateStay(float deltaTime)
    {

    }
    public override void OnStateExit()
    {
        m_targetCharacter.UsingAgroCollider = true;
    }
}
