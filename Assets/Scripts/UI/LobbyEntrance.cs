using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyEntrance : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Entering lobby trigger...");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Exiting lobby trigger...");
        }
    }
}
