using UnityEngine;

public class UsableItem : MonoBehaviour
{
    [SerializeField] private string neededItem;
    [SerializeField] private string successMessage = "Предмет использован успешно!";

    private void OnMouseDown()
    {
        string selectedItem = Inventory.instance.GetSelectedItem();

        if (!string.IsNullOrEmpty(selectedItem) && selectedItem == neededItem)
        {
            if (Inventory.instance.UseItem(neededItem))
            {
                Debug.Log(successMessage);
                IfRight();
            }
        }
        else
        {
            Debug.Log($"Нужен предмет: {neededItem}");
        }
    }

    private void IfRight()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
