using UnityEngine;
using UnityEngine.AI;

public interface INavMeshMovement
{
    void Initialize(NavMeshAgent agent, float speed, Transform transform);
    void Start();
    void Stop();

    void Update();
}
