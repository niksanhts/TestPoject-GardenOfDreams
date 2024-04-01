using UnityEngine;
using UnityEngine.AI;

public class PatrolingMovement : INavMeshMovement
{

    private float _positionOffset = 3f;
    private NavMeshAgent _agent;
    private float _speed;
    private Vector3 _target;
    private float _targetOffset = 0.1f;
    private Transform _transform;

    public void Initialize(NavMeshAgent agent, float speed, Transform transform)
    {
        _agent = agent;
        _speed = speed;
        _transform = transform;
    }

    public void Start()
    {
        _agent.speed = _speed;
        FindTarget();
        _agent.SetDestination(_target);
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }

    public void Update() 
    {
        if (Vector3.Magnitude(_transform.position - _target) > _targetOffset) 
            FindTarget();
    }

    private void FindTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _positionOffset + _transform.position;
        NavMesh.SamplePosition(_target, out NavMeshHit hit, _positionOffset, NavMesh.AllAreas);
        _target = hit.position;

        _agent.SetDestination(_target);
    } 
}
