
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private PhysicalItem[] _physicalItems;

    public void SpawnRandom()
        => Instantiate(_physicalItems[Random.Range(0, _physicalItems.Length)], transform.position, Quaternion.identity);
}
