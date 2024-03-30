using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _ammount;

    public void Initialize(IReadOnlySlotData slot)
    {
        _icon.sprite = slot.Icon;
        _name.text = slot.Name;
        SetAmmount(slot.Ammount);
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
}
