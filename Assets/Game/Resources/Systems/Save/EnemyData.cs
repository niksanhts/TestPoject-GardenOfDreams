using System;
using UnityEngine;

[Serializable]
public struct EnemyData
{
    public MyVector3 _position { get; private set; }
    public float _health { get; private set; }

    public EnemyData(MyVector3 position, float health) 
    {
        _position = position;
        _health = health;
    }
}
