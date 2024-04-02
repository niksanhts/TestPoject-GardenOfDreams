using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState
{
    protected NavMeshAgent _agent;

    public EnemyState(NavMeshAgent agent)
    {
        _agent = agent;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}

