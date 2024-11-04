using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    public bool inRange = false;
    public enum GraveState { Empty, Filled }

    public enum GraveLootTier { Common, Uncommon, Rare, Epic }

    public GraveState graveState = GraveState.Empty;
    public GraveLootTier graveLootTier = GraveLootTier.Common;

    private void Update()
    {
        
    }

    public void Dig()  
    {
        if (inRange == true)
        {
            if (graveState == GraveState.Empty)
            {
                // TODO - Add message to player using UI?
                print("This grave is empty.");
            }
            else
            {
                // TODO - Add loot to player inventory
                print("You found some loot!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
}