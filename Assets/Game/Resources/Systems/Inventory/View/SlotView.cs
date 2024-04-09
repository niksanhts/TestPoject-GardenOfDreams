using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _ammount;
    [SerializeField] private Button _removeButton;

    protected int _ammountInt;

    private IItemRemover _itemRemover;

    public void Initialize(IReadOnlySlotData slot, IItemRemover itemRemover)
    {
        _icon.sprite = SpriteList.Instance.GetSprite(slot.SpriteName);
        _name.text = slot.Name;
        SetAmmount(slot.Ammount);

        _itemRemover = itemRemover;
    }

    public void SetAmmount(int ammount) 
    {
        if (ammount < 1)
            throw new System.Exception(nameof(ammount));

        _ammountInt = ammount;

        if (_ammountInt == 1)
            _ammount.text = "";
        else
            _ammount.text = _ammountInt.ToString();
    }

    private void OnEnable()
    {
        _removeButton.gameObject.SetActive(false);
        _removeButton.onClick.AddListener(RemoveItem);
    }

    private void OnDisable()
    {
        _removeButton.onClick.RemoveAllListeners();
    }

    private void RemoveItem()
    {
        _itemRemover.RemoveItem(_name.text, _ammountInt);
    }
}
