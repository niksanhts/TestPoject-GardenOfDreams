using System;
using UnityEngine;

public class PlayerObserver : MonoBehaviour
{
    public Action<bool> PlayerIsInFirstRadius;
    public Action PlayerIsInSecondRadius;

    [SerializeField, Min(1)] private float _firstRadious;
    [SerializeField] private float _secondRadious;
    [SerializeField] private LayerMask _searchLayer;

    private Collider2D[] _overlapResults = new Collider2D[4];

    private void Update()
    {
        if (Physics2D.OverlapCircleNonAlloc(transform.position, _firstRadious, _overlapResults) != 0)
        {
            if (Physics2D.OverlapCircleNonAlloc(transform.position, _secondRadious, _overlapResults) != 0)
            {
                PlayerIsInSecondRadius?.Invoke();
            }
            else
            {
                PlayerIsInFirstRadius?.Invoke(true);
            }
        }
        else 
        {
            PlayerIsInFirstRadius?.Invoke(false);
        }
    }
}
