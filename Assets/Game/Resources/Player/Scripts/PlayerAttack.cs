using UnityEngine;

public class PlayerAttack : Attack
{
    [SerializeField] private Item _bulletItem;


    private IBulletStorage _bulletStorage;
    private InputHandler _inputHandler;

    public void Initialize(IBulletStorage bulletStorage, InputHandler inputHandler)
    {
        _bulletStorage = bulletStorage;
        _inputHandler = inputHandler;
        _inputHandler.AttackButtonEntered += TryAttack;
    }

    protected override void OnAttackPerformed()
    {
        base.OnAttackPerformed();
        _bulletStorage.TakeBullet(_bulletItem);
    }

    private void TryAttack() 
    {
        if (_bulletStorage.CountBullets(_bulletItem) == 0) 
        {
            return;
        }

        PerformAttack();
    }

    private void OnDisable()
        => _inputHandler.AttackButtonEntered -= TryAttack;
}
