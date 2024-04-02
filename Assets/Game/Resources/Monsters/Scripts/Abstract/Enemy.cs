using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(EnemyStateController))]

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyConfig _config;
    [SerializeField] private HealthBar _healthBar;

    private Health _health;
    private Attack _attack;
    private EnemyStateController _stateController;

    private void Start()
    {
        _health = GetComponent<Health>();
        _attack = GetComponent<Attack>();
        _stateController = GetComponent<EnemyStateController>();

        _health.Initialize(_config.MaxHealth);
        _attack.Initialize(_config.AttackConfig);
        _healthBar.Initialize(_health);
        _stateController.Initialize(_config.ChasingSpeed, _config.PatrolingSpeed);
    }
}
