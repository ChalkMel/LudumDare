using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
   [SerializeField] private Inventory inventory;
    public Image[] slots;

    void Awake()
    {
        inventory = this;
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].sprite == null)
            {
                slots[i].sprite = item.itemIcon;
                slots[i].color = Color.white;
                Debug.Log("Предмет добавлен в ячейку " + i);
                return;
            }
        }
        Debug.Log("Инвентарь полон!");
    }
}
