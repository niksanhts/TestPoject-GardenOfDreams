using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public Action<float> HealthChanged;

    [SerializeField] private UnityEvent TakedDamage;
    [SerializeField] private UnityEvent OnDeath;

    private float _maxHealth;
    private float _currentHealth;


    public void Initialize(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float value)
    {
        if (value < 0) 
        {
            throw new System.Exception(nameof(value));
        }

        if (_currentHealth < value)
        {
            Die();
            return;
        }
            
        _currentHealth -= value;

        print(_currentHealth);

        HealthChanged?.Invoke(_currentHealth / _maxHealth);
        TakedDamage?.Invoke();
    }

    private void Die() 
    {
        OnDeath?.Invoke();
        HealthChanged?.Invoke(0);
    }
}
