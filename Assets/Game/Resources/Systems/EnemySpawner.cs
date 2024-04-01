using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private int _numberOfEnemies = 5;
    [SerializeField] private float _spawnRadius = 10f;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < _numberOfEnemies; i++)
        {
            Vector3 randomPosition = GetRandomPosition();

            if (randomPosition != Vector3.zero)
            {
                var enemy = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)], randomPosition, Quaternion.identity);
                var navMeshAgent = enemy.GetComponent<NavMeshAgent>();
                if (navMeshAgent != null)
                {
                    navMeshAgent.Warp(randomPosition);
                }
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitCircle * _spawnRadius;
        randomDirection += transform.position;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _spawnRadius, NavMesh.AllAreas);
        return hit.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
    }
}

