using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    private INavMeshMovement _movement;

    private NavMeshAgent _agent;
    private PlayerObserver _playerObserver;

    private PatrolingMovement _patrolingMovement;
    private ChasingMovement _chasingMovement;

    private bool Inited = false;

    public void Initialize(PlayerObserver playerObserver, float chasingSpeed, float patrolingSpeed)
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerObserver = playerObserver;

        _patrolingMovement = new PatrolingMovement();
        _chasingMovement = new ChasingMovement();

        _patrolingMovement.Initialize(_agent, patrolingSpeed, transform);
        _chasingMovement.Initialize(_agent, chasingSpeed, transform);

        _playerObserver.PlayerIsInFirstRadius += ChoosePattern;

        _movement = _patrolingMovement;
        _movement.Start();

        Inited = true;
    }

    private void Update()
    {
        if (!Inited) 
            return;

        _movement.Update();
    }

    private void OnDisable()
    {
        _playerObserver.PlayerIsInFirstRadius -= ChoosePattern;
    }

    private void ChoosePattern(bool playerIsNear)
    {
        if (playerIsNear && _movement == _chasingMovement)
            return;
        if (!playerIsNear && _movement == _patrolingMovement)
            return;

        _movement.Stop();
        _movement = playerIsNear ? _chasingMovement : _patrolingMovement;
        _patrolingMovement.Start();
    }


}
