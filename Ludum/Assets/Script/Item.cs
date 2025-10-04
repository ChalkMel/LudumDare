using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;

    private void OnMouseDown()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (inventory != null)
        {
            inventory.AddItem(this);
            Destroy(gameObject);
        }
    }
}

