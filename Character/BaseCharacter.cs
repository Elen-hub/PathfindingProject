using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECharacterState
{
    Idle,
    Attack,
    Move,
    Death,
    Chase,
}
public enum EAllyType
{
    Friend,
    Enermy,
}
public class BaseCharacter : MonoBehaviour
{
    [SerializeField]
    protected ECharacterState m_characterState;
    public EAllyType Ally;
    protected Dictionary<ECharacterState, BaseState> m_stateMethodDic = new Dictionary<ECharacterState, BaseState>();
    protected Animator m_animator;
    CircleCollider2D m_agroCollider;
    public bool UsingAgroCollider { set { m_agroCollider.enabled = value; } }
    protected StatFunction m_stat;
    protected FlockingAgent m_agent;
    protected AStarAgent m_astarAgent;
    public AStarAgent GetAStarAgent { get { return m_astarAgent; } }
    public Stat GetStat { get { return m_stat.GetStat; } }
    public float CurrHP { get { return m_stat.CurrHP; } }
    public float HPPercent { get { return m_stat.HPPercent; } }
    public BaseCharacter Target;
    public Vector3 MoveAxis;
    float m_attackElapsedTime;
    bool m_isPossibleAttack;

    bool m_isStun;
    float m_stunTime;
    float m_stunElapsedTime;
    bool m_isNuckBack;
    float m_nuckBackTime;
    float m_nuckBackElapseTime;
    Vector2 m_nuckBackPos;
    float m_nuckBackForce;
    public void SetStun(float stunTime)
    {
        m_stunElapsedTime = 0;
        m_stunTime = stunTime;
        m_isStun = true;
    }
    public void SetNuckBack(float nuckBackTime, Vector2 nuckBackPos, float nuckBackForce)
    {
        m_nuckBackElapseTime = 0;
        m_nuckBackTime = nuckBackTime;
        m_nuckBackPos = nuckBackPos;
        m_nuckBackForce = nuckBackForce;
        m_isNuckBack = true;
    }
    public FlockingAgent GetFlockingAgent { get { return m_agent; } }

    protected List<BaseCharacter> m_targetList = new List<BaseCharacter>();
    public List<BaseCharacter> GetTargetList { get { return m_targetList; } }
    public string SetAnimatiorTrigger
    {
        set { m_animator.SetTrigger(value); }
    }
    public bool IsPossibleBehavior
    {
        get { return m_isPossibleAttack; }
        set { 
            if(!value) { m_attackElapsedTime = m_stat.GetStat.AttackSpeed; }
            m_isPossibleAttack = value; }
    }
    public float SetAttackElapsedTime { set { m_attackElapsedTime = value; m_isPossibleAttack = false; } }
    public ECharacterState State
    {
        get { return m_characterState; }
        set
        {
            if (m_characterState == value)
                return;

            if (m_characterState == ECharacterState.Death)
                return;

            m_stateMethodDic[m_characterState].OnStateExit();
            m_characterState = value;
            m_stateMethodDic[m_characterState].OnStateEnter();
        }
    }
    public void FindTarget()
    {
        for (int i = m_targetList.Count - 1; i >= 0; --i)
        {
            if (m_targetList[i].State == ECharacterState.Death || m_targetList[i].Ally == Ally)
            {
                m_targetList.Remove(m_targetList[i]);
                continue;
            }
            Target = m_targetList[i];
            break;
        }
    }
    protected void Update()
    {
        if(m_characterState != ECharacterState.Death)
        {
            if (Target != null)
            {
                if (Target.State == ECharacterState.Death)
                {
                    m_targetList.Remove(Target);
                    Target = null;
                    FindTarget();
                }
            }

            if (!m_isPossibleAttack)
            {
                m_attackElapsedTime -= Time.deltaTime;
                if (m_attackElapsedTime < 0)
                    m_isPossibleAttack = true;
            }

            m_stat.NextFrame(Time.deltaTime);
            m_animator.SetInteger("State", (int)State);
        }

        if(m_isStun || m_isNuckBack)
        {
            if(m_isStun)
            {
                m_stunElapsedTime += Time.deltaTime;
                if(m_stunElapsedTime > m_stunTime)
                    m_isStun = false;
            }
            if(m_isNuckBack)
            {
                m_nuckBackElapseTime += Time.deltaTime;
                transform.position += (Vector3)m_nuckBackPos * m_nuckBackForce * Time.deltaTime;
                if (m_nuckBackElapseTime > m_nuckBackTime)
                    m_isNuckBack = false;
            }
            return;
        }
        m_stateMethodDic[m_characterState].OnStateStay(Time.deltaTime);
    }
    public virtual BaseCharacter Init(int groupHandle, float agentDensity, Stat baseStat, BaseState idle, BaseState attack, BaseState move, BaseState death, BaseState chase = null)
    {
        m_animator = GetComponent<Animator>();

        m_stateMethodDic.Add(ECharacterState.Idle, idle);
        m_stateMethodDic.Add(ECharacterState.Attack, attack);
        m_stateMethodDic.Add(ECharacterState.Move, move);
        m_stateMethodDic.Add(ECharacterState.Death, death);

        m_agroCollider = gameObject.AddComponent<CircleCollider2D>();
        m_agroCollider.radius = 3;
        m_agroCollider.isTrigger = true;

        m_stat = new StatFunction(baseStat);
        m_agent = new FlockingAgent(this, groupHandle, agentDensity);
        CharacterMng.Instance.AddGroupMember(groupHandle, this);

        m_astarAgent = gameObject.AddComponent<AStarAgent>();

        return this;
    }
    public virtual void ReceiveDamage(float damage)
    {
        damage -= m_stat.GetStat.DefensivePoint;
        if (damage <= 0)
            return;
        m_stat.CurrHP -= damage;
        if (m_stat.CurrHP <= 0)
        {
            m_stat.CurrHP = 0;
            State = ECharacterState.Death;
            m_animator.Play("Death");
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        BaseCharacter character = collision.GetComponent<BaseCharacter>();
        if (!character)
            return;
        if (m_targetList.Contains(character))
            return;
        if (character.State == ECharacterState.Death)
            return;
        if (Ally != character.Ally)
        {
            m_targetList.Add(character);
            if (Target == null)
                Target = character;
        }
    }
}
