using UnityEngine;

public interface IReadOnlySlotData
{
    ItemType Type { get; }
    string SpriteName { get; }
    string Name { get; }
    int Ammount { get; }
}
