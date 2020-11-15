using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Stat
{
    public float HP;
    public float DefensivePoint;
    public float AttackRange;
    public float AttackSpeed;
    public float AttackDamage;
    public float MoveSpeed;
    public float RecoveryHP;
    public static Stat operator + (Stat a, Stat b)
    {
        Stat stat = new Stat()
        {
            AttackRange = a.AttackRange + b.AttackRange,
            AttackSpeed = a.AttackSpeed + b.AttackSpeed,
            AttackDamage = a.AttackDamage + b.AttackDamage,
            MoveSpeed = a.MoveSpeed + b.MoveSpeed,
            HP = a.HP + b.HP,
        };
        return stat;
    }
}
public class StatFunction
{
    Stat m_baseStat;
    Stat m_equipStat;
    public Stat GetStat { get { return m_baseStat + m_equipStat; } }
    public float CurrHP;
    public float HPPercent { get { return CurrHP / (m_baseStat.HP + m_equipStat.HP); } }
    float m_elapsedTime;
    public StatFunction(Stat baseStat)
    {
        m_baseStat = baseStat;
        m_equipStat = new Stat();
        CurrHP = GetStat.HP;
    }
    public void ResetHP()
    {
        CurrHP = GetStat.HP;
    }
    public void NextFrame(float deltaTime)
    {
        m_elapsedTime += deltaTime;
        if(m_elapsedTime > 5)
        {
            m_elapsedTime = 0;
            CurrHP = Mathf.Clamp(CurrHP + m_baseStat.RecoveryHP + m_equipStat.RecoveryHP, 0, m_baseStat.HP + m_equipStat.HP);
        }
    }
}
