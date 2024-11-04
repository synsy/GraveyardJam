using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    private Transform toolImageParent;
    public Item[] basicTools;


    //this is supposed to equip the tool to the player when its equiped in hotbar cant really figure out a good system on how to set all the tools to not 
    //be active when hotbar empty.
    private void Start()
    {
        GiveBasicTools();
    }
    private void Awake()
    {
        
        toolImageParent = transform.Find("Items");
    }
    void Update()
    {
        Item item = InventoryManager.Instance.GetSelectedItem(false);
        if (item != null)
        {
           
            ActivateToolImage(item.name);
        }

        else
        {
            DeactivateToolImage();
        }


    }
   
    private void ActivateToolImage(string toolName)
    {
        foreach (Transform toolImage in toolImageParent)
        {
            toolImage.gameObject.SetActive(toolImage.name == toolName);
        }
    }

    private void DeactivateToolImage()
    {
        foreach (Transform toolImage in toolImageParent)
        {
            toolImage.gameObject.SetActive(false);
        }
    }
    public void GiveBasicTools()
    {
        Debug.Log("tool");
        for (int i = 0; i < basicTools.Length; i++)
        {
            Item item = basicTools[i];
            InventoryManager.Instance.AddItem(item);
        }
        
    }


}
