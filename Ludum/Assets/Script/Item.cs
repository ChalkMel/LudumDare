using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;
    public string itemDescription;

    private void OnMouseDown()
    {
        Inventory.instance.AddItem(this);
        Destroy(gameObject);
    }
}