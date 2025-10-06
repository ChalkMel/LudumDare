using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;
    public string itemDescription;

    private GameObject DialogWindow;
    private TMP_Text textDialogue;
    [SerializeField] private GameObject cutScene;
    [SerializeField] private string interact = "";
    private void Start()
    {
        DialogWindow = GameObject.FindGameObjectWithTag("DialogWindow");
        textDialogue = DialogWindow.GetComponentInChildren<TMP_Text>();

    }

    private void OnMouseDown()
    {
        Inventory.instance.AddItem(this);
        DialogWindow.SetActive(true);
        textDialogue.text = interact;
        cutScene.SetActive(true);
        Destroy(gameObject);
        if (itemName == "WeddingBand" || itemName == "BabyToy" || itemName == "ToyCar" || itemName == "Ticket" || itemName == "CarPart" || itemName == "Drawing")
        {
            CountUI countUI = FindFirstObjectByType<CountUI>();
            countUI.countClues--;
            countUI.counter.text = countUI.countClues.ToString();
        }
    }
}
