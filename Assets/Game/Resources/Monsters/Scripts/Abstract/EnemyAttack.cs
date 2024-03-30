public class EnemyAttack : Attack
{
    private PlayerObserver _playerObserver;

    public override void Initialize(AttackConfig config, PlayerObserver playerObserver)
    {
        base.Initialize(config);

        _playerObserver = playerObserver;
        _playerObserver.PlayerIsInSecondRadius += PerformAttack;
    }

    private void OnDisable()
    {
        _playerObserver.PlayerIsInSecondRadius -= PerformAttack;
    }


}
