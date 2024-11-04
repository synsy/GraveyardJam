using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    public bool inRange = false;
    public enum GraveState { Empty, Filled }


    public enum GraveLootTier { Common, Uncommon, Rare, Epic, Legendary }
    public Item tool;

    public Item[] lootItems;

    public GraveState graveState = GraveState.Filled;
    public GraveLootTier graveLootTier;
    public GameObject openGrave;
    public GameObject audioManager;
    public AudioManager audioScript;

    private void OnEnable()
    {
        audioManager = GameObject.Find("AudioManager");
        audioScript = audioManager.GetComponent<AudioManager>();
    }
    void Awake()
    {
        audioManager = GameObject.Find("AudioManager");
        audioScript = audioManager.GetComponent<AudioManager>();
        openGrave.SetActive(false);
        
        graveState = GraveState.Filled;
        graveLootTier = GetRandomLootTier();
    }
    private void Update()
    {


        tool = InventoryManager.Instance.GetSelectedItem(false);

    }

    public void Dig()  
    {

      
            if (graveState == GraveState.Empty)
            {

                Debug.Log("empty");
            }
            else
            {
                
                
                graveState = GraveState.Empty;
                openGrave.SetActive(true);
                
                StartCoroutine(Digging());
                
            }
        
    }
    public IEnumerator Digging()
    {
        Player.instance.canMove = false;
        yield return new WaitForSeconds(0.0f);
        Player.instance.canMove = true;
        GenerateLoot();
        audioScript.LootSfx();

    }
    private GraveLootTier GetRandomLootTier()
    {
        int randomValue = Random.Range(0, 1000);

        if(randomValue < 400)
        {
            return GraveLootTier.Common;
        }
        else if(randomValue < 650)
        {
            return GraveLootTier.Uncommon;
        }
        else if(randomValue < 850)
        {
            return GraveLootTier.Rare;
        }
        else if(randomValue < 999)
        {
            return GraveLootTier.Epic;
        }
        else
        {
            return GraveLootTier.Legendary;
        }

    }

    public void GenerateLoot()
    {
        Debug.Log("generate");
        switch (graveLootTier)
        {
            case GraveLootTier.Common:
                if (tool.name == "UpgradedShovel")
                {
                    AddScrapLoot();
                    AddScrapLoot();
                    
                }
                else if (tool.name == "AmazingShovel")
                {
                    AddGemLoot();
                    AddScrapLoot();

                }
                else
                {
                    AddScrapLoot();
                }
                print("Common loot added to inventory.");
                break;
            case GraveLootTier.Uncommon:
                if (tool.name == "UpgradedShovel")
                {
                    AddGemLoot();
                    AddGemLoot();
                    
                }
                else if (tool.name == "AmazingShovel")
                {
                    AddLoot(7);
                    AddGemLoot();

                } 
                else
                {
                    AddGemLoot();
                    AddScrapLoot();
                }
                print("Uncommon loot added to inventory.");
                break;
            case GraveLootTier.Rare:
                if (tool.name == "UpgradedShovel")
                {
                    AddLoot(7);
                    AddLoot(8);
                }
                else if (tool.name == "AmazingShovel")
                {
                    AddLoot(8);
                    AddLoot(9);
                }
                else
                {
                    AddGemLoot();
                    AddGemLoot();

                }
                print("Rare loot added to inventory.");
                break;
            case GraveLootTier.Epic:
                if (tool.name == "UpgradedShovel")
                {
                    AddLoot(9);
                    AddLoot(10);
                }
                else if (tool.name == "AmazingShovel")
                {
                    AddLoot(9);
                    AddGemLoot();
                    AddLoot(10);
                }
                else
                {
                    AddLoot(8);
                    AddGemLoot();
                }
                print("Epic loot added to inventory.");
                break;
            case GraveLootTier.Legendary:
                AddLoot(11);

                break;
        }
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
       
        if(other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
    private void AddScrapLoot()
    {
        int randomValue = Random.Range(0, 3);
        Debug.Log(randomValue);
      
        
        Item scrap = lootItems[randomValue];
        InventoryManager.Instance.AddItem(scrap);
        
    }
    private void AddGemLoot()
    {
        int randomValue = Random.Range(3, 7);

        Item gem = lootItems[randomValue];
        InventoryManager.Instance.AddItem(gem);
    }
    private void AddLoot(int id)
    {
        Item lootToAdd = lootItems[id];
        InventoryManager.Instance.AddItem(lootToAdd);
    }
}