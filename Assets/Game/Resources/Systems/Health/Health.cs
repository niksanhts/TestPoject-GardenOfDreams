using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public Action<float> HealthChanged;

    [SerializeField] private bool _save;
    [SerializeField] private UnityEvent TakedDamage;
    [SerializeField] private UnityEvent OnDeath;

    private float _maxHealth;
    private float _currentHealth;


    public void Initialize(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;

        if (_save)
        {
            try
            {
                //_currentHealth = (float)Storage.Load(gameObject.name, "health");
            }
            catch
            {
                _currentHealth = _maxHealth;
            }
        }

        if (_currentHealth == 0)
            _currentHealth = _maxHealth;

        HealthChanged?.Invoke(_currentHealth / _maxHealth);
    }

    private void OnDisable()
    {
        //if (_save)
        //    Storage.Save(gameObject.name, "health", _currentHealth);
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
