using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject 
{
    public bool stackable;
    public Sprite image;
    public bool isTool;
    public bool isConsumable;
    public int value;
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
