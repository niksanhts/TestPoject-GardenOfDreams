using UnityEngine;
using UnityEngine.AI;

public class ChasingMovement : INavMeshMovement
{
    private NavMeshAgent _agent;
    private Transform _target;

    private float _speed;


    public void Initialize(NavMeshAgent agent, float speed, Transform transform)
    {
        _speed = speed;
        _agent = agent;
        _target = GameObject.FindAnyObjectByType<Player>().transform;
    }

    public void Start()
    {
        _agent.speed = _speed;
        _agent.Move(_target.position);
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }

    public void Update()
    {

    }
}
