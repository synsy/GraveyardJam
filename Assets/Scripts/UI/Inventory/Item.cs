using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    
    public Sprite image;

    public bool stackable;
    public bool isTool;
    public bool isConsumable;
    public bool isLoot;
    public bool isMelee;
    public bool isGun;
    public bool isLifeSaver;

    public bool unsellable;
    public int value;
    public int cost;
    public int damage;
    public int health;
    public int speedGain;

    public string itemName;
    public string description;
   
    public bool extraLife;
   
    //only gameplay
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    public enum ItemType
    {
        Tool
    }
    public enum ActionType
    {
        Dig,
        Mine
    }
}
