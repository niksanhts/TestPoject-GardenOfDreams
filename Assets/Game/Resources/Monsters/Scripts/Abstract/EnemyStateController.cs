using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour
{
    
    [SerializeField] private float _chaseRange = 4f;
    [SerializeField] private float _minRemainingDistance = 0.5f;
    [SerializeField] private float _newPointRange = 4f;

    private EnemyState _currentState;
    private NavMeshAgent _agent;
    private Transform _playerTransform;
    private PatrolState _patrolState;
    private ChaseState _chaseState;

    private bool isInit = false;

    public void Initialize(float chasingSpeed, float patrolingSpeed)
    {
        isInit = true;

        _playerTransform = FindAnyObjectByType<Player>().transform;
        _agent = GetComponent<NavMeshAgent>();

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _chaseState = new ChaseState(_agent, _playerTransform, chasingSpeed);
        _patrolState = new PatrolState(_agent, _minRemainingDistance, _newPointRange, patrolingSpeed);
    }

    private void Update()
    {
        if (isInit == false) 
        {
            return;
        }

        if (Vector3.Distance(transform.position, _playerTransform.position) <= _chaseRange)
        {
            SetState(_chaseState);
        }
        else if (!(_currentState is PatrolState))
        {
            SetState(_patrolState);
        }

        _currentState.OnUpdate();
    }

    public void SetState(EnemyState state)
    {
        _currentState?.OnExit();
        _currentState = state;
        _currentState.OnEnter();
    }

    private void OnDrawGizmos()
    {
        if (_agent == null) return;

        if (_agent.pathPending) return;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_agent.destination, 0.1f);
        //Gizmos.DrawSphere(transform.position, _chaseRange);
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
        Gizmos.color = Color.yellow;
        var previousCorner = transform.position;
        foreach (var corner in _agent.path.corners)
        {
            Gizmos.DrawLine(previousCorner, corner);
            previousCorner = corner;
        }

        
    }
}

