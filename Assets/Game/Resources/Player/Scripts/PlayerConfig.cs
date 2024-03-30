using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Player")]
public class PlayerConfig : ScriptableObject
{
    public float Speed => _speed;
    public float MaxHealth => _maxHealth;
    public AttackConfig AttackConfig => _attackConfig;

    [SerializeField] private AttackConfig _attackConfig;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxHealth;
}
