using System;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    private AttackConfig _config;
    private Collider2D[] _targets = new Collider2D[8];
    private float _lastAttackTime;

    public virtual void Initialize(AttackConfig config)
    {
        _config = config;
        _lastAttackTime = Time.time;
    }

    public void PerformAttack() 
    {
        PerformAttack(OnAttackPerformed);
    }

    public void PerformAttack(Action callback) 
    {
        if (_lastAttackTime + (1 / _config.Rate) >= Time.time)
            return;

        _lastAttackTime = Time.time;

        PerformOverlap();

        List<Collider2D> filteredTargets = new List<Collider2D>(8);
        FilterTargets(filteredTargets);

        if (_config.AreaAttack)
            CauseDamageToAllTargets(filteredTargets);
        else
            CauseDamageToTarget(filteredTargets);

        callback();

        print("attack ");
    }

    protected virtual void OnAttackPerformed() 
    {

    }


    protected virtual void CauseDamageToTarget(IEnumerable<Collider2D> targets) 
    {
        CauseDamageToClosestTarget(targets);
    }

    protected virtual void CauseDamageToAllTargets(IEnumerable<Collider2D> targets) 
    {
        foreach (var target in targets) 
        {
            target.TryGetComponent(out IDamageable damageable);
            damageable.TakeDamage(_config.Damage);
        }
    }



    private void FilterTargets(List<Collider2D> filteredTargets)
    {
        foreach (var target in _targets)
        {
            if (target == null || target.gameObject.TryGetComponent(out IDamageable damageable) == false)
                continue;
            //if (Physics2D.Linecast(transform.position, target.transform.position, _config.ObstacleLayer) == false)
                //continue;

            filteredTargets.Add(target);
        }
    }

    private void PerformOverlap()
        => Physics2D.OverlapCircleNonAlloc(transform.position, _config.Radious, _targets, _config.AttackableLayers);

    private void CauseDamageToClosestTarget(IEnumerable<Collider2D> targets) 
    {
        GameObject closestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Collider2D target in targets)
        {
            GameObject enemy = target.gameObject;
            float distance = Vector3.Distance(enemy.transform.position, currentPosition);

            if (distance < minDistance)
            {
                closestEnemy = enemy;
                minDistance = distance;
            }
        }

        closestEnemy.TryGetComponent(out IDamageable damageable);
        damageable.TakeDamage(_config.Damage);
    }

    private void OnDrawGizmos()
    {
        try
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _config.Radious);
        }
        catch 
        {
            return;
        }
        
    }



}
