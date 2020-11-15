using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_SkillAttack : BaseState
{
    new BossMonster m_targetCharacter;
    public Attack_SkillAttack(BaseCharacter target) : base(target)
    {
        m_targetCharacter = target as BossMonster;
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
        if (!m_targetCharacter.IsPossibleBehavior)
            return;

        if (Vector2.Distance(m_targetCharacter.transform.position, m_targetCharacter.Target.transform.position) > m_targetCharacter.GetStat.AttackRange)
        {
            m_targetCharacter.State = ECharacterState.Chase;
            return;
        }

        if (m_targetCharacter.GetSkill.PossibleSkill())
        {
            float durationTime = 0;
            m_targetCharacter.SetAnimatiorTrigger = "Attack";
            m_targetCharacter.GetSkill.UseSkill(out durationTime);
            m_targetCharacter.SetAttackElapsedTime = durationTime;
            return;
        }
        m_targetCharacter.IsPossibleBehavior = false;
        m_targetCharacter.SetAnimatiorTrigger = "Attack";
        m_targetCharacter.Target.ReceiveDamage(m_targetCharacter.GetStat.AttackDamage);
    }
    public override void OnStateExit()
    {

    }
}
