using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public string Name => _name;
    public Sprite Icon => _icon;
    public GameObject Prefab => _prefab;
    public ItemType Type => _itemType;

    [SerializeField] private ItemType _itemType;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _prefab;

}

public enum ItemType 
{
    Weapon,
    Armor,
    Bullet,
}
