﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Shouting : BaseSkill
{
    float m_range = 3f;
    float m_nuckBackTime = 0.5f;
    float m_nuckBackForce = 2;
    public override BaseSkill Init(BaseCharacter caster)
    {
        base.Init(caster);
        m_coolTime = 7.5f;
        return this;
    }
    public override void UseSkill(out float durationTime)
    {
        base.UseSkill(out durationTime);

        Collider2D[] col = Physics2D.OverlapCircleAll(m_caster.transform.position, m_range);
        if (col != null)
        {
            for (int i = 0; i < col.Length; ++i)
            {
                BaseCharacter character = col[i].GetComponent<BaseCharacter>();
                if (character == null)
                    continue;

                if (character.State == ECharacterState.Death)
                    continue;

                if (m_caster.Ally == character.Ally)
                    continue;

                character.SetNuckBack(m_nuckBackTime, character.transform.position - transform.position, m_nuckBackForce);
            }
        }
    }
}
