using UnityEngine;
using UnityEngine.EventSystems;

public class ShelfSlot : MonoBehaviour, IPointerClickHandler
{
    [Header("Настройки слота")]
    public int slotIndex; // Индекс этого слота на полке
    public string requiredItemType; // Тип предмета, который можно разместить здесь

    [Header("Визуальные элементы")]
    public GameObject itemVisual; // Визуальное представление предмета
    public SpriteRenderer itemSpriteRenderer; // Для отображения спрайта предмета

    private ShelfController shelfController;
    private string currentItemName = "";

    void Start()
    {
        shelfController = GetComponentInParent<ShelfController>();
        if (shelfController == null)
        {
            Debug.LogError("ShelfController не найден в родительских объектах!");
        }

        // Скрываем визуал предмета при старте
        if (itemVisual != null)
            itemVisual.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Левый клик - разместить предмет
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            TryPlaceItem();
        }
        // Правый клик - забрать предмет
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            TryTakeItem();
        }
    }

    // Попытка разместить предмет
    private void TryPlaceItem()
    {
        // Если слот уже занят, выходим
        if (!string.IsNullOrEmpty(currentItemName))
            return;

        // Получаем выбранный предмет из инвентаря
        Inventory inventory = Inventory.instance;
        if (inventory != null)
        {
            string selectedItem = inventory.GetSelectedItem();
            if (!string.IsNullOrEmpty(selectedItem))
            {
                // Проверяем, можно ли разместить этот предмет здесь
                if (string.IsNullOrEmpty(requiredItemType) || selectedItem == requiredItemType)
                {
                    // Пытаемся разместить предмет на полке
                    if (shelfController.PlaceItem(selectedItem, slotIndex))
                    {
                        currentItemName = selectedItem;
                        UpdateVisuals(selectedItem);

                        // Удаляем предмет из инвентаря
                        inventory.UseItem(selectedItem);
                    }
                }
                else
                {
                    Debug.Log($"Этот предмет нельзя разместить здесь! Требуется: {requiredItemType}");
                }
            }
        }
    }

    // Попытка забрать предмет
    private void TryTakeItem()
    {
        // Если слот пустой, выходим
        if (string.IsNullOrEmpty(currentItemName))
            return;

        // Пытаемся забрать предмет с полки
        if (shelfController.TakeItem(slotIndex))
        {
            string itemToReturn = currentItemName;
            currentItemName = "";
            ClearVisuals();

            // Возвращаем предмет в инвентарь (нужно создать временный предмет)
            ReturnItemToInventory(itemToReturn);
        }
    }

    // Обновление визуального представления
    private void UpdateVisuals(string itemName)
    {
        if (itemVisual != null)
            itemVisual.SetActive(true);

        // Здесь можно добавить логику для изменения спрайта в зависимости от предмета
        // Например, загружать спрайт по имени предмета
    }

    // Очистка визуального представления
    private void ClearVisuals()
    {
        if (itemVisual != null)
            itemVisual.SetActive(false);
    }

    // Возврат предмета в инвентарь
    private void ReturnItemToInventory(string itemName)
    {
        // Создаем временный объект предмета для добавления в инвентарь
        GameObject tempItem = new GameObject("Temp_" + itemName);
        Item itemComponent = tempItem.AddComponent<Item>();
        itemComponent.itemName = itemName;

        // Здесь нужно установить соответствующий спрайт для предмета
        // itemComponent.itemIcon = Resources.Load<Sprite>("Items/" + itemName);

        Inventory inventory = Inventory.instance;
        if (inventory != null)
        {
            inventory.AddItem(itemComponent);
        }

        Destroy(tempItem);
    }

    // Метод для внешнего управления (например, из инспектора)
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