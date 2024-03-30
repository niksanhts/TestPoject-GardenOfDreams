using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _barFill;
    private Health _health;

    public void Initialize(Health health)
    {
        _health = health;
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        if (_health != null)
            _health.HealthChanged -= OnHealthChanged;
    }
    private void OnHealthChanged(float value)
        => _barFill.fillAmount = value;

}
