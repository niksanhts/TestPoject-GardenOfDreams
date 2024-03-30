
using System;
using UnityEngine;

[Serializable]
public struct PlayerData
{
    public Inventory Inventory { get; private set; }
    public MyVector3 Position { get; private set; }
    public float Health { get; private set; }

    public PlayerData(Inventory inventory, MyVector3 position, float health) 
    {
        Inventory = inventory;
        Position = position;
        Health = health;
    }
}
