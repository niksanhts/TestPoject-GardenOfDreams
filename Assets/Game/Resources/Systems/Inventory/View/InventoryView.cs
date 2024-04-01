using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private SlotView _slotPrefab;
    [SerializeField] private Transform _parant;
    private List<SlotView> slots = new List<SlotView>();

    private IItemRemover _itemRemover;

    public void Initialize(IItemRemover itemRemover)
    {
        _itemRemover = itemRemover;
    }

    public void AddSlots(IEnumerable<IReadOnlySlotData> slotDatas)
    {
        foreach (var slotData in slotDatas)
            AddSlot(slotData);
    }

    public SlotView AddSlot(IReadOnlySlotData slotData) 
    {
        var slot = Instantiate(_slotPrefab, _parant);
        slot.name = slotData.Name;
        slot.Initialize(slotData, _itemRemover);
        slots.Add(slot);

        return slot;
    }

    public void RemoveSlot(IReadOnlySlotData slotData) 
        => Destroy(FindSlotByName(slotData.Name));

    public void UpdateSlot(IReadOnlySlotData slotData)
    {
        var slot = FindSlotByName(slotData.Name);
        if (slot == null)
            AddSlot(slotData);
        slot.SetAmmount(slotData.Ammount);
    }

    public SlotView FindSlotByName(string name) 
    {
        foreach (var slot in slots)
        {
            if (slot.name == name)
            {
                return slot;
            }
        }
        return null;
    }
}
