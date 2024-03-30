using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
    [SerializeField] private Item _bulletItem;


    private IBulletStorage _bulletStorage;

    public void Initialize(IBulletStorage bulletStorage)
    {
        _bulletStorage = bulletStorage;
    }

    private void TryAttack() 
    {
        if (_bulletStorage.CountBullets(_bulletItem) == 0) 
        {
            return;
        }

        PerformAttack();
    }

    private void OnEnable()
        => InputHandler.Instance.AttackButtonEntered += PerformAttack;

    private void OnDisable()
        => InputHandler.Instance.AttackButtonEntered -= PerformAttack;
}
