using System;
using System.Collections.Generic;
using static UnityEditor.Progress;

[Serializable]
public class Inventory
{
    public event Action<IReadOnlySlotData> SlotAdded;
    public event Action<IReadOnlySlotData> SlotRemoved;
    public event Action<IReadOnlySlotData> SlotAmmountUpdated;

    private List<InventorySlot> _slots = new List<InventorySlot>();

    public void AddItem(Item item, int ammount) 
    {
        foreach (InventorySlot slot in _slots) 
        {
            if (slot.Name == item.Name) 
            {
                slot.IncreaseAmmount(ammount);
                SlotAmmountUpdated?.Invoke(slot);
                return;
            }
        }

        var newSlot = new InventorySlot(item, ammount);
        _slots.Add(newSlot);
        SlotAdded?.Invoke(newSlot);
    }

    public void RemoveItem(IReadOnlySlotData slotData)
        => RemoveItem(slotData.Name, slotData.Ammount);

    public void RemoveItem(Item item, int ammount)
        => RemoveItem(item.name, ammount);

    public void RemoveItem(string name, int ammount) 
    {
        foreach (InventorySlot slot in _slots)
        {
            if (slot.Name == name)
            {
                slot.DecreaseAmmount(ammount);

                if (slot.Empty)
                {
                    SlotRemoved?.Invoke(slot);
                    _slots.Remove(slot);
                }
                else 
                {
                    SlotAmmountUpdated?.Invoke(slot);
                }

                return;
            }
        }

        throw new Exception(nameof(name));
    }

    public int Count(Item item) 
    {
        foreach (InventorySlot slot in _slots)
        {
            if (slot.Name == item.Name)
            {
                return slot.Ammount;
            }
        }

        return 0;
    }

    public IEnumerable<IReadOnlySlotData> GetAllSlotsData()
    {
        IReadOnlySlotData[] slotDatas = new IReadOnlySlotData[_slots.Count];

        for (int i = 0; i < _slots.Count; i++)
        {
            slotDatas[i] = _slots[i];
        }

        return slotDatas;
    }
}
