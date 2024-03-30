
using System;

public class InventoryController : IBulletStorage
{
    public Action<IReadOnlySlotData> BulletAmmountChanged;

    private InventoryView _inventoryView;
    private Inventory _inventoryModel;

    public InventoryController(InventoryView view) 
    {
        _inventoryModel = new Inventory();
        _inventoryView = view;

        _inventoryModel.SlotAdded += OnSlotAdded;
        _inventoryModel.SlotRemoved += OnSlotRamoved;
        _inventoryModel.SlotAmmountUpdated += OnSlotAmmountUpdated;
    }

    ~InventoryController() 
    {
        _inventoryModel.SlotAdded -= OnSlotAdded;
        _inventoryModel.SlotRemoved -= OnSlotRamoved;
        _inventoryModel.SlotAmmountUpdated -= OnSlotAmmountUpdated;
    }

    public int CountBullets(Item bullets)
    => Count(bullets);

    public void TakeBullet(Item bullet)
        => RemoveItem(bullet, 1);

    public int Count(Item item)
        => _inventoryModel.Count(item);

    public void AddItem(Item item, int ammount)
        => _inventoryModel.AddItem(item, ammount);

    public void RemoveItem(Item item, int ammount)
        => _inventoryModel.Removetem(item, ammount);

    private void OnSlotAdded(IReadOnlySlotData slotData) 
        => _inventoryView.AddSlot(slotData);

    private void OnSlotRamoved(IReadOnlySlotData slotData)
        => _inventoryView.RemoveSlot(slotData);

    private void OnSlotAmmountUpdated(IReadOnlySlotData slotData)
    {
        if (slotData.Type == ItemType.Bullet)
            BulletAmmountChanged?.Invoke(slotData);

        _inventoryView.UpdateSlot(slotData);
    }


}
