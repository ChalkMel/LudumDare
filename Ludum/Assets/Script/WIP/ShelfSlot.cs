using UnityEngine;
using UnityEngine.EventSystems;

public class ShelfSlot : MonoBehaviour, IPointerClickHandler
{
    [Header("��������� �����")]
    public int slotIndex; // ������ ����� ����� �� �����
    public string requiredItemType; // ��� ��������, ������� ����� ���������� �����

    [Header("���������� ��������")]
    public GameObject itemVisual; // ���������� ������������� ��������
    public SpriteRenderer itemSpriteRenderer; // ��� ����������� ������� ��������

    private ShelfController shelfController;
    private string currentItemName = "";

    void Start()
    {
        shelfController = GetComponentInParent<ShelfController>();
        if (shelfController == null)
        {
            Debug.LogError("ShelfController �� ������ � ������������ ��������!");
        }

        // �������� ������ �������� ��� ������
        if (itemVisual != null)
            itemVisual.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // ����� ���� - ���������� �������
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            TryPlaceItem();
        }
        // ������ ���� - ������� �������
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            TryTakeItem();
        }
    }

    // ������� ���������� �������
    private void TryPlaceItem()
    {
        // ���� ���� ��� �����, �������
        if (!string.IsNullOrEmpty(currentItemName))
            return;

        // �������� ��������� ������� �� ���������
        Inventory inventory = Inventory.instance;
        if (inventory != null)
        {
            string selectedItem = inventory.GetSelectedItem();
            if (!string.IsNullOrEmpty(selectedItem))
            {
                // ���������, ����� �� ���������� ���� ������� �����
                if (string.IsNullOrEmpty(requiredItemType) || selectedItem == requiredItemType)
                {
                    // �������� ���������� ������� �� �����
                    if (shelfController.PlaceItem(selectedItem, slotIndex))
                    {
                        currentItemName = selectedItem;
                        UpdateVisuals(selectedItem);

                        // ������� ������� �� ���������
                        inventory.UseItem(selectedItem);
                    }
                }
                else
                {
                    Debug.Log($"���� ������� ������ ���������� �����! ���������: {requiredItemType}");
                }
            }
        }
    }

    // ������� ������� �������
    private void TryTakeItem()
    {
        // ���� ���� ������, �������
        if (string.IsNullOrEmpty(currentItemName))
            return;

        // �������� ������� ������� � �����
        if (shelfController.TakeItem(slotIndex))
        {
            string itemToReturn = currentItemName;
            currentItemName = "";
            ClearVisuals();

            // ���������� ������� � ��������� (����� ������� ��������� �������)
            ReturnItemToInventory(itemToReturn);
        }
    }

    // ���������� ����������� �������������
    private void UpdateVisuals(string itemName)
    {
        if (itemVisual != null)
            itemVisual.SetActive(true);

        // ����� ����� �������� ������ ��� ��������� ������� � ����������� �� ��������
        // ��������, ��������� ������ �� ����� ��������
    }

    // ������� ����������� �������������
    private void ClearVisuals()
    {
        if (itemVisual != null)
            itemVisual.SetActive(false);
    }

    // ������� �������� � ���������
    private void ReturnItemToInventory(string itemName)
    {
        // ������� ��������� ������ �������� ��� ���������� � ���������
        GameObject tempItem = new GameObject("Temp_" + itemName);
        Item itemComponent = tempItem.AddComponent<Item>();
        itemComponent.itemName = itemName;

        // ����� ����� ���������� ��������������� ������ ��� ��������
        // itemComponent.itemIcon = Resources.Load<Sprite>("Items/" + itemName);

        Inventory inventory = Inventory.instance;
        if (inventory != null)
        {
            inventory.AddItem(itemComponent);
        }

        Destroy(tempItem);
    }

    // ����� ��� �������� ���������� (��������, �� ����������)
    public void ForcePlaceItem(string itemName)
    {
        if (shelfController.PlaceItem(itemName, slotIndex))
        {
            currentItemName = itemName;
            UpdateVisuals(itemName);
        }
    }

    public void ForceTakeItem()
    {
        if (shelfController.TakeItem(slotIndex))
        {
            currentItemName = "";
            ClearVisuals();
        }
    }
}