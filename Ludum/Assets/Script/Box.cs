using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Box : MonoBehaviour
{
    [SerializeField] private string neededItem;
    [SerializeField] private string success = "";
    [SerializeField] private string interact = "";
    [SerializeField] private string interactAfter = "Did it";
    [SerializeField] private GameObject key;
    private GameObject DialogWindow;
    private TMP_Text textDialogue;
    private bool picked = false;

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
            if (picked) 
            {
                DialogWindow.SetActive(true);
                textDialogue.text = interactAfter;
            }
            else
            {
                DialogWindow.SetActive(true);
                textDialogue.text = interact;
            }
        }
    }

    private void IfRight()
    {
        if (!picked)
        {
            DialogWindow.SetActive(true);
            textDialogue.text = interact;
            key.SetActive(true);
            picked = true;
    }
}
}
