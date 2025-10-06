using TMPro;
using UnityEngine;

public class UsableItem : MonoBehaviour
{
    [SerializeField] private string neededItem;
    [SerializeField] private string success = "";
    [SerializeField] private string interact = "";
    [SerializeField] private GameObject stool;
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
                Destroy(gameObject);
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
        
        if(neededItem == "Stool")
        {
            stool.SetActive(true );
        }
    }
}
