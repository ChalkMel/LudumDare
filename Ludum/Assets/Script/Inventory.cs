using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public Image[] slots;
    public Button[] buts;
    public string[] itemName;

    private SlotDescription[] slotDescriptions;

    private int selectedSlotIndex = -1;
    private string selectedItemName = "";

    void Awake()
    {
        instance = this;

        slotDescriptions = new SlotDescription[slots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            slotDescriptions[i] = slots[i].GetComponent<SlotDescription>();

            int index = i; 
            buts[i].onClick.AddListener(() => SelectSlot(index));
        }
    }

    public void AddItem(Item item)
    {
        if (item == null)
        {
            Debug.LogError("Item is null!");
            return;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null) continue;

            if (slots[i].sprite == null)
            {
                slots[i].sprite = item.itemIcon;
                itemName[i] = item.itemName;
                slots[i].color = Color.white;

                // Проверяем slotDescriptions[i] на null
                if (slotDescriptions != null && i < slotDescriptions.Length && slotDescriptions[i] != null)
                {
                    slotDescriptions[i].UpdateItemData(item.itemName, item.itemName + " - " + item.itemDescription);
                    Debug.Log("Item data updated in slot " + i);
                }
                else
                {
                    Debug.LogWarning("SlotDescription is null for slot " + i);
                }

                Debug.Log("Предмет добавлен в ячейку " + i);
                return;
            }
        }
        Debug.Log("Инвентарь полон!");
    }

    private void SelectSlot(int slotIndex)
    {
        if (string.IsNullOrEmpty(itemName[slotIndex]))
        {
            return;
        }

        if (selectedSlotIndex != -1)
        {
            slots[selectedSlotIndex].color = Color.white;
        }
        if (selectedSlotIndex == slotIndex)
        {
            DeselectSlot();
            return;
        }

        selectedSlotIndex = slotIndex;
        selectedItemName = itemName[slotIndex];
        slots[slotIndex].color = Color.yellow;

        Debug.Log($"Выбран предмет: {selectedItemName} из слота {slotIndex}");
    }
    public bool UseItem(string requiredItemName)
    {
        if (selectedSlotIndex != -1 && selectedItemName == requiredItemName)
        {
            slots[selectedSlotIndex].sprite = null;
            slots[selectedSlotIndex].color = Color.white;

            if (slotDescriptions[selectedSlotIndex] != null)
            {
                slotDescriptions[selectedSlotIndex].ClearSlot();

            }

            itemName[selectedSlotIndex] = "";

            Debug.Log($"Предмет {selectedItemName} использован!");

            selectedSlotIndex = -1;
            selectedItemName = "";

            return true;
        }
        else
        {
            Debug.Log("Неверный предмет выбран или предмет не выбран!");
            return false;
        }
    }
    public string GetSelectedItem()
    {
        return selectedItemName;
    }
    public void DeselectSlot()
    {
        if (selectedSlotIndex != -1)
        {
            slots[selectedSlotIndex].color = Color.white;
            Debug.Log($"Выделение снято с предмета: {selectedItemName}");
        }

        selectedSlotIndex = -1;
        selectedItemName = "";
    }
    public bool HasItem(string itemNameToCheck)
    {
        for (int i = 0; i < itemName.Length; i++)
        {
            if (itemName[i] == itemNameToCheck)
            {
                return true;
            }
        }
        return false;
    }
}