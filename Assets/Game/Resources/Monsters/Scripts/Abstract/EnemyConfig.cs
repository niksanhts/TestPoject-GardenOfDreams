using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Enemy")]
public class EnemyConfig : ScriptableObject 
{
    public float PatrolingSpeed => _patrolingSpeed;
    public float ChasingSpeed => _chasingSpeed;
    public float MaxHealth => _maxHealth;
    public AttackConfig AttackConfig => _attackConfig;

    [SerializeField] private float _patrolingSpeed;
    [SerializeField] private float _chasingSpeed;
    [SerializeField] private float _maxHealth;
    [SerializeField] private AttackConfig _attackConfig;
}
    