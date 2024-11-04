using UnityEngine;
using UnityEngine.UI;

public class InfoText : MonoBehaviour
{
    public Text tooltipText; // Reference to the UI Text component for the tooltip

    private void Awake()
    {
        // Make sure the tooltip is not visible at the start
        tooltipText.gameObject.SetActive(false);
    }

    public void ShowTooltip(string itemName, string description, int value)
    {
        tooltipText.text = $"{itemName}\n{description}\nValue: {value}";
        tooltipText.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltipText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Optional: Follow mouse position if tooltip is active
        if (tooltipText.gameObject.activeSelf)
        {
            tooltipText.transform.position = Input.mousePosition + new Vector3(10, -10, 0); // Adjust offset as needed
        }
    }
}