using UnityEngine;

public class EnemyAttack : Attack
{
    private float _attackRange;
    private Transform _playerTarnsform;

    public override void Initialize(AttackConfig config)
    {
        base.Initialize(config);
        _attackRange = config.Radious;
        _playerTarnsform = FindAnyObjectByType<Player>().transform;
    }

    private void Update()
    {
        if (Vector3.Distance(_playerTarnsform.position, transform.position) <= _attackRange) 
            PerformAttack();
    }


}
