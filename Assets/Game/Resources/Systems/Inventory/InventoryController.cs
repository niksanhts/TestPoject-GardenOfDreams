
using System;

public class InventoryController : IBulletStorage, IItemRemover
{
    public Action<IReadOnlySlotData> BulletAmmountChanged;

    private InventoryView _inventoryView;
    private Inventory _inventoryModel;

    public InventoryController(InventoryView view) 
    {

        try
        {
            _inventoryModel = (Inventory)Storage.Load("Player", "inventory");
        }
        catch
        {
            _inventoryModel = new Inventory();
        }

        if (_inventoryModel == null)
        {
            _inventoryModel = new Inventory();
        }

        _inventoryView = view;
        _inventoryView.AddSlots(_inventoryModel.GetAllSlotsData());

        _inventoryModel.SlotAdded += OnSlotAdded;
        _inventoryModel.SlotRemoved += OnSlotRamoved;
        _inventoryModel.SlotAmmountUpdated += OnSlotAmmountUpdated;
    }

    ~InventoryController() 
    {
        Storage.Save("Player", "inventory", _inventoryModel);

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

    private void OnSlotRamoved(IReadOnlySlotData slotData)
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
