using UnityEngine;

public class TestInit : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private HealthBar _bar;

    public void Start()
    {
        _health.Initialize(100);
        _bar.Initialize(_health);
    }
}
