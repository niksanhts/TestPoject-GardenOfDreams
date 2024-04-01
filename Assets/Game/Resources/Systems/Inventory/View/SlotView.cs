using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _ammount;
    [SerializeField] private Button _removeButton;

    private IItemRemover _itemRemover;

    public void Initialize(IReadOnlySlotData slot, IItemRemover itemRemover)
    {
        _icon.sprite = slot.Icon;
        _name.text = slot.Name;
        SetAmmount(slot.Ammount);

        _itemRemover = itemRemover;
        _removeButton.onClick.AddListener(RemoveItem);
    }

    public void SetAmmount(int ammount) 
    {
        if (ammount < 1)
            throw new System.Exception(nameof(ammount));

        if (ammount == 1)
            _ammount.text = "";
        else
            _ammount.text = ammount.ToString();
    }

    private void OnEnable()
    {
        _removeButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _removeButton.onClick.RemoveAllListeners();
    }

    private void RemoveItem()
    {
        _itemRemover.RemoveItem(_name.text, int.Parse(_ammount.text));
    }
}
