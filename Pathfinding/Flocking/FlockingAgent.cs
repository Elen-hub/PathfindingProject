using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingGroup
{
    public int GroupHandle;
    BaseCharacter m_owner;
    public List<BaseCharacter> Members = new List<BaseCharacter>();
    public BaseCharacter GetOwner
    {
        get
        {
            if (m_owner == null)
            {
                if (Members.Count == 0)
                    CharacterMng.Instance.RemoveGroup(this);
                else
                    m_owner = Members[0];
            }
            return m_owner;
        }
    }
    public BaseCharacter SetOwner { set { m_owner = value; } }
    public void AddMember(BaseCharacter character)
    {
        character.GetFlockingAgent.GroupHandle = GroupHandle;
        Members.Add(character);
    }
    public FlockingGroup(int handle, BaseCharacter owner)
    {
        Members.Add(owner);
        GroupHandle = handle;
        m_owner = owner;
    }
}
public class FlockingAgent
{
    int m_groupHandle = 0;
    public int GroupHandle { get { return m_groupHandle; } set { m_groupHandle = value; } }
    BaseCharacter m_character;

    // 겹칠 수 있는 거리
    float m_agentDensity;
    // 이웃 거리
    const float m_radius = 1.5f;

    public FlockingAgent(BaseCharacter character, int groupHandle, float density)
    {
        m_character = character;
        m_groupHandle = groupHandle;
        m_agentDensity = density;
    }
    public bool GetAxis(Transform destination, out Vector3 direction)
    {
        direction = Vector3.zero;

        FlockingGroup group = CharacterMng.Instance.GetGroup(m_groupHandle);
        List<Transform> context = new List<Transform>();

        Vector3 characterPos = m_character.transform.position;
        for (int i = 0; i < group.Members.Count; ++i)
        {
            Vector3 targetPos = group.Members[i].transform.position;
            float distance = Vector3.Distance(characterPos, targetPos);
            if (distance <= m_radius)
            {
                if (group.Members[i] == m_character)
                    continue;

                // 겹쳐있다면
                if(distance ==0)
                {
                    goto DestinationCalcul;
                }
                // 만약 너무 가까운 그룹원이 있다면 멀어진다.
                else if(distance < m_agentDensity + group.Members[i].GetFlockingAgent.m_agentDensity)
                {
                    direction += (characterPos - group.Members[i].transform.position).normalized * 3;
                    context.Add(group.Members[i].transform);
                }
            }
        }

        DestinationCalcul:
        // 목적지와 방향백터를 연산한다.
        if (!context.Contains(destination))
        {
            direction += (destination.position - characterPos).normalized;
            context.Add(destination);
        }

        // 방향벡터 합산
        direction /= context.Count;
        return true;
    }
}
