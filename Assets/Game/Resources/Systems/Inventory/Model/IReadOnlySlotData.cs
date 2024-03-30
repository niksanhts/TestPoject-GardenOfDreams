using UnityEngine;

public interface IReadOnlySlotData
{
    ItemType Type { get; }
    Sprite Icon { get; }
    string Name { get; }
    int Ammount { get; }
}
