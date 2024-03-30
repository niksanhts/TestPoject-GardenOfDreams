using System;
using UnityEngine;

[Serializable]
public struct InventorySlot : IReadOnlySlotData
{
    public Sprite Icon => _item.Icon;
    public string Name => _item.Name;
    public int Ammount => _ammount;
    public bool Empty => _ammount == 0;
    public ItemType Type => _item.Type;

    private Item _item;
    private int _ammount;

    public InventorySlot(Item item, int ammount)
    {
        _item = item;
        _ammount = ammount;
    }

    public void IncreaseAmmount(int value) 
    {
        if (value < 1) 
            throw new System.ArgumentOutOfRangeException(nameof(value));

        _ammount += value;
    }

    public void DecreaseAmmount(int value)
    {
        if (value < 1)
            throw new System.ArgumentOutOfRangeException(nameof(value));

        if(_ammount - value <= 0)
            _ammount = 0;

        _ammount -= value;
    }

}
