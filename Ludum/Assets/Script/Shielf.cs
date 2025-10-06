using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class Shielf : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public GameObject itemObject;
        public int correctPosition;
        [HideInInspector] public bool isOnShelf = false;
        [HideInInspector] public int currentSlotIndex = -1;
        [HideInInspector] public Vector3 originalPosition;
        [HideInInspector] public Transform originalParent;
    }
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
    public GameObject[] shelfSlots;

    private InventoryItem selectedItem;
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button gameOverButton;
    [SerializeField] private Button gameFinishButton;
    [SerializeField] private Button finaleButton;
    [SerializeField] private Image image;
    [SerializeField] private GameObject image2;
    [SerializeField] private GameObject finale;

    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = EventSystem.current;
        InitializeItems();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    void InitializeItems()
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            var item = inventoryItems[i];
            if (item.itemObject != null)
            {
                item.originalPosition = item.itemObject.transform.localPosition;
                item.originalParent = item.itemObject.transform.parent;
                item.isOnShelf = false;
                item.currentSlotIndex = -1;
            }
        }
    }

    void HandleClick()
    {
        GameObject clickedObject = GetClickedUIObject();

        if (clickedObject == null) return;

        InventoryItem clickedItem = GetInventoryItem(clickedObject);
        if (clickedItem != null)
        {
            HandleItemClick(clickedItem);
            return;
        }
        int shelfIndex = GetShelfSlotIndex(clickedObject);
        if (shelfIndex != -1 && selectedItem != null)
        {
            PlaceItemOnShelf(shelfIndex);
        }
    }

    void HandleItemClick(InventoryItem item)
    {
        if (selectedItem == item)
        {
            if (item.isOnShelf)
            {
                ReturnItemToInventory(item);
            }
            DeselectItem();
        }
        else if (selectedItem == null)
        {
            SelectItem(item);
        }
        else
        {
            DeselectItem();
            SelectItem(item);
        }
    }

    GameObject GetClickedUIObject()
    {
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        if (results.Count > 0)
        {
            return results[0].gameObject;
        }
        return null;
    }

    InventoryItem GetInventoryItem(GameObject clickedObject)
    {
        foreach (var item in inventoryItems)
        {
            if (item.itemObject == clickedObject)
            {
                return item;
            }
        }
        return null;
    }

    int GetShelfSlotIndex(GameObject clickedObject)
    {
        for (int i = 0; i < shelfSlots.Length; i++)
        {
            if (shelfSlots[i] == clickedObject)
            {
                return i;
            }
        }
        return -1;
    }

    void SelectItem(InventoryItem item)
    {
        selectedItem = item;

        Image itemImage = item.itemObject.GetComponent<Image>();
        if (itemImage != null)
        {
             itemImage.color = Color.yellow;
        }

        Debug.Log($"Выбран предмет: {item.itemObject.name}");
    }

    void DeselectItem()
    {
        if (selectedItem == null) return;

        Image itemImage = selectedItem.itemObject.GetComponent<Image>();
        if (itemImage != null)
        {
            if (!selectedItem.isOnShelf || selectedItem.currentSlotIndex != selectedItem.correctPosition)
            {
                 itemImage.color = Color.white;
            }
            else
            {
                itemImage.color = Color.white;
            }
        }

        selectedItem = null;
    }

    void PlaceItemOnShelf(int shelfIndex)
    {
        if (selectedItem == null) return;

        foreach (var item in inventoryItems)
        {
            if (item.isOnShelf && item.currentSlotIndex == shelfIndex && item != selectedItem)
            {
                ReturnItemToInventory(item);
                break;
            }
        }

        selectedItem.itemObject.transform.SetParent(shelfSlots[shelfIndex].transform);
        selectedItem.itemObject.transform.localPosition = Vector3.zero;
        selectedItem.itemObject.transform.localScale = Vector3.one;
        selectedItem.isOnShelf = true;
        selectedItem.currentSlotIndex = shelfIndex;

        Debug.Log($"Предмет {selectedItem.itemObject.name} размещен на полке в позиции {shelfIndex}");

        CheckSolution();

        DeselectItem();
    }

    void ReturnItemToInventory(InventoryItem item)
    {
        item.itemObject.transform.SetParent(item.originalParent);
        item.itemObject.transform.localPosition = item.originalPosition;
        item.itemObject.transform.localScale = Vector3.one;
        item.isOnShelf = false;
        item.currentSlotIndex = -1;

        Debug.Log($"Предмет {item.itemObject.name} возвращен в инвентарь");
    }

    public bool CheckCorrectPlacement()
    {
        foreach (var item in inventoryItems)
        {
            if (!item.isOnShelf || item.currentSlotIndex != item.correctPosition)
            {
                return false;
            }
        }
        return true;
    }

    public bool AllItemsPlaced()
    {
        foreach (var item in inventoryItems)
        {
            if (!item.isOnShelf)
            {
                return false;
            }
        }
        return true;
    }

    public bool HasIncorrectPlacement()
    {
        foreach (var item in inventoryItems)
        {
            if (item.isOnShelf && item.currentSlotIndex != item.correctPosition)
            {
                return true;
            }
        }
        return false;
    }

    public void CheckSolution()
    {
        if (CheckCorrectPlacement())
        {
            panel.SetActive(true);
            gameFinishButton.onClick.AddListener(End);
        }
        else if (AllItemsPlaced() && HasIncorrectPlacement())
        {
                gameOverPanel.SetActive(true);
            gameOverButton.onClick.AddListener(Restart);
        }
    }
    public void ResetAllItems()
    {
        foreach (var item in inventoryItems)
        {
            if (item.isOnShelf)
            {
                ReturnItemToInventory(item);
            }
        }
        DeselectItem();
    }
    void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void End()
    {
        image2.SetActive(true);
    }
}