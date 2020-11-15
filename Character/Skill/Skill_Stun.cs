using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Stun : BaseSkill
{
    float m_range = 3;
    float m_stunTime = 2.5f;
    public override BaseSkill Init(BaseCharacter caster)
    {
        base.Init(caster);
        m_coolTime = 7;
        return this;
    }
    public override void UseSkill(out float durationTime)
    {
        base.UseSkill(out durationTime);

        Collider2D[] col = Physics2D.OverlapCircleAll(m_caster.transform.position, m_range);
        for (int i = 0; i < col.Length; ++i)
        {
            BaseCharacter character = col[i].GetComponent<BaseCharacter>();
            if (character == null)
                continue;

            if (character.State == ECharacterState.Death)
                continue;

            if (m_caster.Ally == character.Ally)
                continue;

            character.SetStun(m_stunTime);
        }
    }
}
