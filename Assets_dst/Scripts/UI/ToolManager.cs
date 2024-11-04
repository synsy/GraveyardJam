using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    private Transform toolImageParent;
    
    //this is supposed to equip the tool to the player when its equiped in hotbar cant really figure out a good system on how to set all the tools to not 
    //be active when hotbar empty.
    private void Awake()
    {
        toolImageParent = transform.Find("Items");
    }
    void Update()
    {
     Item item = InventoryManager.Instance.GetSelectedItem(false);
     if (item != null && item.isTool)
        {
            useTool(item);
            ActivateToolImage(item.name);
        }
 
     else
        {
            Debug.Log(item);
            DeactivateToolImage();
        }
   
        
    }
    public void useTool(Item item)
    {
       


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

}
    