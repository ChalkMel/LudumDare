using TMPro;
using UnityEngine;

public class UsableItem : MonoBehaviour
{
    [SerializeField] private string neededItem;
    [SerializeField] private string success = "";
    [SerializeField] private string interact = "";
    private GameObject DialogWindow;
    private TMP_Text textDialogue;

    private void Awake()
    {

        DialogWindow = GameObject.FindGameObjectWithTag("DialogWindow");
        textDialogue = DialogWindow.GetComponentInChildren<TMP_Text>();
    }
    private void OnMouseDown()
    {
        string selectedItem = Inventory.instance.GetSelectedItem();

        if (!string.IsNullOrEmpty(selectedItem) && selectedItem == neededItem)
        {
            if (Inventory.instance.UseItem(neededItem))
            {
                DialogWindow.SetActive(true);
                textDialogue.text = success;
                IfRight();
            }
        }
        else
        {
            DialogWindow.SetActive(true);
            textDialogue.text = interact;
        }
    }

    private void IfRight()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
}
