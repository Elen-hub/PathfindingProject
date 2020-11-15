using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public struct InstanceHandler
{
    public Stat Stat;
    public string Path;
    public float AgentDensity;
    public int IdleState;
    public int AttackState;
    public int MoveState;
    public int DeathState;
    public int ChaseState;
}

public class CharacterMng : TSingleton<CharacterMng>
{
    public enum ECharacters
    {
        Main,
        Mouse,
        Rabit,
        Deer,
        Bear,
        Alligator,
    }
    Dictionary<int, FlockingGroup> m_group = new Dictionary<int, FlockingGroup>();
    PlayerCharacter m_mainCharacter;
    public PlayerCharacter MainCharacter { get { return m_mainCharacter; } }
    Dictionary<ECharacters, InstanceHandler> m_instanceHandlerDic = new Dictionary<ECharacters, InstanceHandler>();
    public int GetBlankGroupNum {
        get {
            int i = 0;
            while (true)
            {
                ++i;
                if (m_group.ContainsKey(i))
                    continue;

                return i;
            }
        }
    }
    public void Init()
    {
        TextAsset Asset = Resources.Load<TextAsset>("CharaterStats");
        JSONNode Node = JSON.Parse(Asset.text);
        for (int i = 0; i < Node.Count; ++i)
        {
            Stat stat = new Stat()
            {
                HP = float.Parse(Node[i]["HP"]),
                AttackRange = float.Parse(Node[i]["AttackRange"]),
                AttackSpeed = float.Parse(Node[i]["AttackSpeed"]),
                AttackDamage = float.Parse(Node[i]["AttackDamage"]),
                MoveSpeed = float.Parse(Node[i]["MoveSpeed"]),
                RecoveryHP = float.Parse(Node[i]["Recovery"])
            };
            InstanceHandler handler = new InstanceHandler()
            {
                Stat = stat,
                Path = Node[i]["Path"].Value,
                AgentDensity = float.Parse(Node[i]["AgentDensity"]),
                IdleState = int.Parse(Node[i]["IdleState"]),
                AttackState = int.Parse(Node[i]["AttackState"]),
                MoveState = int.Parse(Node[i]["MoveState"]),
                DeathState = int.Parse(Node[i]["DeathState"]),
                ChaseState = int.Parse(Node[i]["ChaseState"]),
            };
            m_instanceHandlerDic.Add((ECharacters)i, handler);
        }
        m_mainCharacter = InstantiateCharacter(ECharacters.Main, new Vector2(0, -25f), 0) as PlayerCharacter;
    }
    public BaseCharacter InstantiateCharacter(ECharacters instanceType, Vector2 pos, int groupNumber)
    {
        BaseCharacter outCharacter = Instantiate(Resources.Load<BaseCharacter>(m_instanceHandlerDic[instanceType].Path), pos, Quaternion.identity);

        // Selection StateMachine
        BaseState idle = null, attack = null, move = null, death = null, chase = null;
        switch (m_instanceHandlerDic[instanceType].IdleState) {
            case 0: idle = new Idle_Player(outCharacter); break;
            case 1: idle = new Idle_Enermy(outCharacter); break;
        }
        switch (m_instanceHandlerDic[instanceType].AttackState) {
            case 0: attack = new Attack_Player(outCharacter); break;
            case 1: attack = new Attack_ChaseAttack(outCharacter); break;
            case 2: attack = new Attack_SkillAttack(outCharacter); break;
        }
        switch (m_instanceHandlerDic[instanceType].MoveState){
            case 0: move = new Move_Player(outCharacter); break;
            case 1: move = new Move_Enermy(outCharacter); break;
        }
        switch (m_instanceHandlerDic[instanceType].DeathState){
            case 0: death = new Death_Default(outCharacter); break;
        }
        switch (m_instanceHandlerDic[instanceType].ChaseState) {
            case 1: chase = new Chase_Enermy(outCharacter); break;
            default: break;
        }

        outCharacter.Init(groupNumber, m_instanceHandlerDic[instanceType].AgentDensity, m_instanceHandlerDic[instanceType].Stat, idle, attack, move, death, chase);
        return outCharacter;
    }
    public void AddGroupMember(int group, BaseCharacter member)
    {
        if (!m_group.ContainsKey(group))
            m_group.Add(group, new FlockingGroup(group, member));
        else
            m_group[group].AddMember(member);
    }
    public void RemoveGroupMember(int group, BaseCharacter member)
    {
        m_group[group].Members.Remove(member);
    }
    public void RemoveGroup(FlockingGroup group)
    {
        m_group.Remove(group.GroupHandle);
    }
    public FlockingGroup GetGroup(int number)
    {
        return m_group[number];
    }
}
