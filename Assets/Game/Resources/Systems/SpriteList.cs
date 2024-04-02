using System.Collections.Generic;
using UnityEngine;

public class SpriteList : MonoBehaviour
{
    public static SpriteList Instance;

    [SerializeField] private Sprite[] _sprites;

    private Dictionary<string, Sprite> _spriteMap = new Dictionary<string, Sprite>();

    private void Awake()
    {
        if (Instance != null) 
        {
            Destroy(this);
            return;
        }

        Instance = this;

        FillSpriteMap();
    }

    public Sprite GetSprite(string name) 
    {
        return _spriteMap[name];
    }

    private void FillSpriteMap() 
    {
        foreach (var sprite in _sprites) 
        {
            _spriteMap.Add(sprite.name, sprite);
        }
    }
}
