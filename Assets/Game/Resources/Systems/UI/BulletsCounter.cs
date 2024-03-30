using TMPro;
using UnityEngine;

public class BulletsCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMeshPro;

    public void ChangeAmmount(int ammount)
    {
        if (ammount < 0)
            throw new System.Exception(nameof(ammount));

        _textMeshPro.text = ammount.ToString();
    }
}
