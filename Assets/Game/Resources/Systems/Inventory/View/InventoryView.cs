using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private SlotView _slotPrefab;
    [SerializeField] private Transform _parant;
    private List<SlotView> slots = new List<SlotView>();

    public void AddSlot(IReadOnlySlotData slotData) 
    {
        var slot = Instantiate(_slotPrefab, _parant);
        slot.name = slotData.Name;
        slot.Initialize(slotData);
        slots.Add(slot);
    }

    public void RemoveSlot(IReadOnlySlotData slotData) 
        => Destroy(FindSlotByName(slotData.Name));
    public void UpdateSlot(IReadOnlySlotData slotData) 
        => FindSlotByName(slotData.Name)?.SetAmmount(slotData.Ammount);

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
