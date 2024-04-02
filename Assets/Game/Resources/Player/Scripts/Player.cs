using UnityEngine;

[RequireComponent(typeof(ItemPicker))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attack))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private BulletsCounter _bulletsCounter;

    private ItemPicker _itemPicker;
    private InventoryController _inventoryController;
    private Movement _movement;
    private Health _health;
    private PlayerAttack _attack;

    private void Start()
    {
        _inventoryController = new InventoryController(_inventoryView);

        _movement = GetComponent<Movement>();
        _health = GetComponent<Health>();
        _itemPicker = GetComponent<ItemPicker>();
        _attack = GetComponent<PlayerAttack>();

        _movement.Initialize(_playerConfig.Speed, _inputHandler);
        _health.Initialize(_playerConfig.MaxHealth);
        _healthBar.Initialize(_health);
        _inventoryView.Initialize(_inventoryController);
        _itemPicker.Initialize(_inventoryController);
        _attack.Initialize(_playerConfig.AttackConfig);
        _attack.Initialize(_inventoryController, _inputHandler);

        _bulletsCounter.ChangeAmmount(_inventoryController.CountByType(ItemType.Bullet));
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
