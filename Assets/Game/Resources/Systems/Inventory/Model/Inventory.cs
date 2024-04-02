using System;
using System.Collections.Generic;
using static UnityEditor.Progress;

[Serializable]
public class Inventory
{
    public event Action<IReadOnlySlotData> SlotCreated;
    public event Action<IReadOnlySlotData> SlotRemoved;
    public event Action<IReadOnlySlotData> SlotAmmountUpdated;

    private List<InventorySlot> _slots = new List<InventorySlot> { };

    public Inventory() 
    {
        Load();
    }

    public void AddItem(Item item, int ammount) 
    {
        if (_slots == null)
        {
            _slots = new List<InventorySlot>();
            CreateSlot(item, ammount);
            Save();
            return;
        }

        foreach (InventorySlot slot in _slots) 
        {
            if (slot.Name == item.Name) 
            {
                slot.IncreaseAmmount(ammount);
                SlotAmmountUpdated?.Invoke(slot);
                Save();
                return;
            }
        }

        CreateSlot(item, ammount);
        Save();
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
                Save();
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
        if (_slots == null)
            return new IReadOnlySlotData[] { };

        IReadOnlySlotData[] slotDatas = new IReadOnlySlotData[_slots.Count];

        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i] != null)
                slotDatas[i] = _slots[i];
        }

        return slotDatas;
    }

    public int CountByType(ItemType type) 
    {
        int result = 0;

        if (_slots == null)
            return result;

        foreach (var slot in _slots)
        {
            if (slot.Type == type)
                result += slot.Ammount;
        }

        return result;
    }


    private void Save() 
    {
        Storage.Save("Player", "inventory", _slots);
    }

    private InventorySlot CreateSlot(Item item, int ammount ) 
    {
        var newSlot = new InventorySlot(item, ammount);
        _slots.Add(newSlot);
        SlotCreated?.Invoke(newSlot);
        return newSlot;
    }

    private void Load() 
    {
        try 
        {
            _slots = (List<InventorySlot>) Storage.Load("Player", "inventory");
        }
        catch 
        {
            _slots = new List<InventorySlot> { };
        }
    }
}
