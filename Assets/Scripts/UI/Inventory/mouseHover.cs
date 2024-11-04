using UnityEngine;
using UnityEngine.EventSystems;

public class ItemHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryItem inventoryItem;  // Reference to the InventoryItem component
    public InfoText infoText;
    private void Start()
    {
        infoText = FindObjectOfType<InfoText>(); // Finds the InfoText in the scene
    }
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inventoryItem != null && inventoryItem.item != null && infoText != null)
        {
            // Call the TooltipManager to show the item information
            infoText.ShowTooltip(inventoryItem.item.itemName, inventoryItem.item.description, inventoryItem.item.value);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (infoText != null)
        {
            infoText.HideTooltip(); // Call the TooltipManager to hide the tooltip
        }
    }
}
