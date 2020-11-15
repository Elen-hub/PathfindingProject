using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

struct ARequestHandler
{
    public AStarAgent Agent;
    public Vector3 Start;
    public Vector3 End;
   
    public ARequestHandler(AStarAgent agent, Vector3 start, Vector3 end)
    {
        Agent = agent;
        Start = start;
        End = end;
    }
}
public class AStarMng : TSingleton<AStarMng>
{
    Queue<ARequestHandler> m_requestQueue = new Queue<ARequestHandler>();
    ARequestHandler m_currentHandler;
    AStarPathfinding m_pathFinding;
    bool m_isDone;
    public AGrid Grid { set { m_pathFinding.Grid = value; } }
    public void RequestPathfinding(AStarAgent agent, Vector3 start, Vector3 end)
    {
        ARequestHandler handler = new ARequestHandler(agent, start, end);
        m_requestQueue.Enqueue(handler);
        NextProcess();
    }
    public void CompleteProcess(Stack<Vector3> path)
    {
        m_currentHandler.Agent.PathFindingCallback(path);
        m_isDone = false;
        NextProcess();
    }
    void NextProcess()
    {
        if(!m_isDone && m_requestQueue.Count > 0)
        {
            m_isDone = true;
            m_currentHandler = m_requestQueue.Dequeue();
            StartCoroutine(m_pathFinding.SearchPath(m_currentHandler.Start, m_currentHandler.End));
        }
    }

    public Stack<Vector3> TestSearch(Vector3 start, Vector3 end)
    {
        return m_pathFinding.TestSearch(start, end);
    }
    public void Init()
    {
        m_pathFinding = new AStarPathfinding();
    }
}

