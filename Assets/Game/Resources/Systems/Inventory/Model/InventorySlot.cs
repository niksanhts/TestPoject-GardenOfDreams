using System;
using UnityEngine;

[Serializable]
public class InventorySlot : IReadOnlySlotData
{
    public string SpriteName { get; private set; }
    public string Name { get; private set; }
    public int Ammount { get; private set; }
    public bool Empty => Ammount == 0;
    public ItemType Type { get; private set; }



    public InventorySlot(Item item, int ammount)
    {
        SpriteName = item.Icon.name;
        Name = item.Name;
        Type = item.Type;
        Ammount = ammount;
    }

    public void IncreaseAmmount(int value) 
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        var newAmmount = Ammount + value;
        Ammount = newAmmount;
    }

    public void DecreaseAmmount(int value)
    {
        if (value < 1) 
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        } 

        if (Ammount - value <= 0)
        {
            Ammount = 0;
            return;
        }

        Ammount -= value;
    }

}
