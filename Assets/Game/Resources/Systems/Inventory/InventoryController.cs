
using System;


public class InventoryController : IBulletStorage, IItemRemover
{
    public Action<IReadOnlySlotData> BulletAmmountChanged;

    private InventoryView _inventoryView;
    private Inventory _inventoryModel;

    public InventoryController(InventoryView view) 
    {
        _inventoryModel = new Inventory();

        _inventoryView = view;
        _inventoryView.AddSlots(_inventoryModel.GetAllSlotsData());

        _inventoryModel.SlotCreated += OnSlotAdded;
        _inventoryModel.SlotRemoved += OnSlotRemoved;
        _inventoryModel.SlotAmmountUpdated += OnSlotAmmountUpdated;
    }

    ~InventoryController() 
    {
        _inventoryModel.SlotCreated -= OnSlotAdded;
        _inventoryModel.SlotRemoved -= OnSlotRemoved;
        _inventoryModel.SlotAmmountUpdated -= OnSlotAmmountUpdated;
    }

    public int CountByType(ItemType type)
        => _inventoryModel.CountByType(type);

    public int CountBullets(Item bullets)
        => Count(bullets);

    public void TakeBullet(Item bullet)
        => RemoveItem(bullet, 1);

    public int Count(Item item)
        => _inventoryModel.Count(item);

    public void AddItem(Item item, int ammount)
        => _inventoryModel.AddItem(item, ammount);

    public void RemoveItem(Item item, int ammount)
        => _inventoryModel.RemoveItem(item, ammount);

    public void RemoveItem(IReadOnlySlotData slotData)
        => _inventoryModel.RemoveItem(slotData);

    public void RemoveItem(string name, int ammount)
        => _inventoryModel.RemoveItem(name, ammount);

    private void OnSlotAdded(IReadOnlySlotData slotData)
    {
        if (slotData.Type == ItemType.Bullet)
            BulletAmmountChanged?.Invoke(slotData);

        _inventoryView.AddSlot(slotData);
    }

    private void OnSlotRemoved(IReadOnlySlotData slotData)
    {
        if (slotData.Type == ItemType.Bullet)
            BulletAmmountChanged?.Invoke(slotData);

        _inventoryView.RemoveSlot(slotData);
    }

    private void OnSlotAmmountUpdated(IReadOnlySlotData slotData)
    {
        if (slotData.Type == ItemType.Bullet)
            BulletAmmountChanged?.Invoke(slotData);

        _inventoryView.UpdateSlot(slotData);
    }

    
}
