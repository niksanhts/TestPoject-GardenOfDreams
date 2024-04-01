using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    private AttackConfig _config;
    private Collider2D[] _targets = new Collider2D[16];
    private float _lastAttackTime;

    public virtual void Initialize(AttackConfig config)
    {
        _config = config;
        _lastAttackTime = Time.time;
    }

    public virtual void Initialize(AttackConfig config, PlayerObserver playerObserver)
        => Initialize(config);

    public void PerformAttack() 
    {
        if (_lastAttackTime + (1 / _config.Rate) >= Time.time)
            return;

        _lastAttackTime = Time.time;

        PerformOverlap();

        FilterAndCouseDamage();

        print("atak by " + gameObject.name + " target: ");
    }

    private void PerformOverlap()
        => Physics2D.OverlapCircleNonAlloc(transform.position, _config.Radious, _targets, _config.AttackableLayers);


    private void FilterAndCouseDamage() 
    {
        foreach (var target in _targets)
        {
            if (target == null || target.gameObject.TryGetComponent(out IDamageable damageable) == false)
                continue;
            if (Physics2D.Linecast(transform.position, target.transform.position, _config.ObstacleLayer) == false)
                continue;

            damageable.TakeDamage(_config.Damage);
            print("damage couse" + _config.Damage);

            if (_config.AreaAttack == false)
                return;
        }
    }

    
    
}
