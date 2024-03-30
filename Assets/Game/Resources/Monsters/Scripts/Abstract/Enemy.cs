using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(PlayerObserver))]

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyConfig _config;

    private Health _health;
    private Attack _attack;
    private PlayerObserver _playerObserver;
    private EnemyMover _enemyMover;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _attack = GetComponent<Attack>();
        _playerObserver = GetComponent<PlayerObserver>();
        _enemyMover = GetComponent<EnemyMover>();

        _health.Initialize(_config.MaxHealth);
        _attack.Initialize(_config.AttackConfig, _playerObserver);
        _enemyMover.Initialize(_playerObserver, _config.ChasingSpeed, _config.PatrolingSpeed);
        
        
    }
}
