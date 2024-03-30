using UnityEngine;

[RequireComponent(typeof(ItemPicker))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attack))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private HealthBar _healthBar;

    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private BulletsCounter _bulletsCounter;

    private ItemPicker _itemPicker;
    private InventoryController _inventoryController;
    private Movement _movement;
    private Health _health;
    private Attack _attack;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _health = GetComponent<Health>();
        _itemPicker = GetComponent<ItemPicker>();
        _attack = GetComponent<Attack>();

        _movement.Initialize(_playerConfig.Speed);
        _health.Initialize(_playerConfig.MaxHealth);
        _healthBar.Initialize(_health);
        _attack.Initialize(_playerConfig.AttackConfig);

        _inventoryController = new InventoryController(_inventoryView);
        
        _itemPicker.Initialize(_inventoryController);

        _inventoryController.BulletAmmountChanged += UpdateBulletsCounter;
    }

    private void OnDisable()
    {
        _inventoryController.BulletAmmountChanged -= UpdateBulletsCounter;
    }

    private void UpdateBulletsCounter(IReadOnlySlotData slotData) 
    {
        _bulletsCounter.ChangeAmmount(slotData.Ammount);
    }
}
