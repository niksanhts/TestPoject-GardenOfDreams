using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    private InventoryController _inventoryController;

    public void Initialize(InventoryController inventory)
        => _inventoryController = inventory;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PhysicalItem item))
            _inventoryController.AddItem(item.PickUp(), item.Ammount);
    }
}
