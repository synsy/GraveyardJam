using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIUiOpener : MonoBehaviour
{
    public GameObject shop;
    public GameObject BJ;
    public bool lobby;
    public bool hasExited;
    private void Awake()
    {
        
        lobby = true;
        shop = GameObject.FindWithTag("BuyMenu");
        shop.SetActive(false);
    }
    private void OnTriggerStay2D (Collider2D other)
    {
        if (other.CompareTag("Shop"))
        {
            other.transform.GetChild(1).gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                if(shop.activeSelf == false)
                    ToggleShop(true);
                    
            }
        }
        if (other.CompareTag("BJ"))
        {
            other.transform.GetChild(1).gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                if (shop.activeSelf == false)
                    ToggleBJ(true);

            }
        }

        if (other.CompareTag("Door"))
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            if(Input.GetKey(KeyCode.E) && hasExited == false)
            {
                SceneManager.instance.LoadSceneWithFade("Level1");
                lobby = false;
        
            }
        }

        if(other.CompareTag("LobbyEntrance"))
        {
            if (other.transform.childCount > 0)
            {
                other.transform.GetChild(0).gameObject.SetActive(true);
                if (Input.GetKey(KeyCode.E))
                {
                    GameManager.instance.currentTimeOfDay = GameManager.TimeOfDay.Day;
                    SceneManager.instance.LoadSceneWithFade("Lobby");
                    lobby = true;
                    hasExited = true;
                }
            }
        }

        if(other.CompareTag("Bed"))
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            if(Input.GetKey(KeyCode.E) && GameManager.instance.currentTimeOfDay == GameManager.TimeOfDay.Day)
            {
                hasExited = false;
                GameManager.instance.AdvanceDay();
            }
        }
    }

    private void OnTriggerExit2D (Collider2D other)
    {
        if (other.CompareTag("Shop"))
        {
            other.transform.GetChild(1).gameObject.SetActive(false);
            ToggleShop(false);
        }
        if (other.CompareTag("BJ"))
        {
            other.transform.GetChild(1).gameObject.SetActive(false);
            ToggleBJ(false);
        }

        if (other.CompareTag("Bed"))
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
        }

        if(other.CompareTag("Door"))
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (other.CompareTag("LobbyEntrance") && other.transform.childCount > 0)
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void ToggleShop(bool toggle)
    {
        shop.SetActive(toggle);
    }
    void ToggleBJ(bool toggle)
    {
        BJ.SetActive(toggle);
    }
}
