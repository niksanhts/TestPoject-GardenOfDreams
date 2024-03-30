using UnityEngine;

public class PhysicalItem : MonoBehaviour
{
    public int Ammount => _ammount;

    [SerializeField] private Item _item;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private int _ammount = 1;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _item.Icon;
    }

    public Item PickUp()
    {
        Destroy(gameObject);
        return _item;
    }
}
