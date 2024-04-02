using UnityEngine;
using UnityEngine.AI;

public class ChaseState : EnemyState
{
    private Transform _playerTransform;
    private float _speed;

    public ChaseState(NavMeshAgent agent, Transform playerTransform, float speed) : base(agent)
    {
        _playerTransform = playerTransform;
        _speed = speed;
    }

    public override void OnEnter()
    {
        _agent.speed = _speed;
        _agent.SetDestination(_playerTransform.position);
    }

    public override void OnUpdate()
    {
        _agent.SetDestination(_playerTransform.position);
    }

    public override void OnExit()
    {
    }
}

