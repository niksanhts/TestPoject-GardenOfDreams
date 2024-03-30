using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Attack")]
public class AttackConfig : ScriptableObject
{
    public float Rate => _rate;
    public float Damage => _damage;
    public float Radious => _radius;
    public LayerMask AttackableLayers => _attackableLayers;
    public LayerMask ObstacleLayer => _obstacleLayers;
    public bool AreaAttack => _areaAttack;

    [SerializeField] private float _rate;
    [SerializeField] private float _damage;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _attackableLayers;
    [SerializeField] private LayerMask _obstacleLayers;
    [SerializeField] private bool _areaAttack;
}
