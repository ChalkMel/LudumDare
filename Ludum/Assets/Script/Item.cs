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
    [SerializeField] AudioSource play;
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
        if (itemName == "WeddingBand" || itemName == "Bottle" || itemName == "Toy Car" || itemName == "Ticket" || itemName == "Torn Clothes" || itemName == "Drawing")
        {
            CountUI countUI = FindFirstObjectByType<CountUI>();
            countUI.countClues--;
            countUI.counter.text = countUI.countClues.ToString();
            play.enabled = true;
            play.Play();
        }
    }
}
