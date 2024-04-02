using UnityEngine;
using UnityEngine.AI;

public class PatrolState : EnemyState
{
    private float _minRemainingDistance;
    private float _positionOffset = 4f;
    private Vector3 nextDestination;
    private float _speed;

    public PatrolState(NavMeshAgent agent, float minRemainingDistance,
        float _positionOffset, float speed) : base(agent)
    {
        _speed = speed;
        _minRemainingDistance = minRemainingDistance;
    }

    public override void OnEnter()
    {
        _agent.speed = _speed;
        PickNextPoint();
    }

    public override void OnUpdate()
    {
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            PickNextPoint();
        }
    }

    public override void OnExit()
    {

    }

    void PickNextPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _positionOffset + _agent.transform.position;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _positionOffset, NavMesh.AllAreas);

        _agent.SetDestination(hit.position);
    }
}

