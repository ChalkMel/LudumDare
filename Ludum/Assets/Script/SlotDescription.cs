using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SlotDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private string itemDescription = "Описание предмета";
    public string ItemName { get; set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(ItemName))
        {
            descriptionPanel.SetActive(true);
            descriptionText.text = itemDescription;
            itemNameText.text = ItemName;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionPanel.SetActive(false);
    }
    public void UpdateItemData(string name, string description = "")
    {
        ItemName = name;
        if (!string.IsNullOrEmpty(description))
        {
            itemDescription = description;
        }
    }

    public void ClearSlot()
    {
        ItemName = "";
        descriptionPanel.SetActive(false);
    }
}