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
        print("atak by " + gameObject.name);

        PerformOverlap();

        var targets = FilterTargets();

        if (targets.Length < 1)
            return;

        if (_config.AreaAttack)
            foreach (var target in targets)
                CauseDamage(target);
        else 
            CauseDamage(targets[0]);

        
    }

    protected virtual IDamageable ChooseTarget(IDamageable[] damageables)
        => damageables[0];

    private void CauseDamage(IDamageable damageable)
        => damageable.TakeDamage(_config.Damage);

    private void PerformOverlap()
        => Physics2D.OverlapCircleNonAlloc(transform.position, _config.Radious, _targets, _config.AttackableLayers);


    private IDamageable[] FilterTargets() 
    {
        List<IDamageable> targets = new List<IDamageable>();
        var hasObstacle = false;


        foreach (var target in _targets) 
        {
            if (target == null)
                continue;

            hasObstacle = Physics2D.Linecast(transform.position, target.transform.position, _config.ObstacleLayer.value);
            if (target.TryGetComponent(out IDamageable damageable) && !hasObstacle) 
                targets.Add(damageable);
        }

        return targets.ToArray();
    }

    
    
}
