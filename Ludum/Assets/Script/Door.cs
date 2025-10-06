using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private GameObject DialogWindow;
    private TMP_Text textDialogue;
    [SerializeField] private string interact = "";
    [SerializeField] private string interactSuc = ""; 
    [SerializeField] Animation open;
    [SerializeField] AudioSource play;
    private void Awake()
    {
        DialogWindow = GameObject.FindGameObjectWithTag("DialogWindow");
        textDialogue = DialogWindow.GetComponentInChildren<TMP_Text>();
    }
    void OnMouseDown()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (inventory.HasItem("Ticket") && inventory.HasItem("WeddingBand") && inventory.HasItem("ToyCar") && inventory.HasItem("Drawing") && inventory.HasItem("BabyToy") && inventory.HasItem("CarPart"))
        {
            textDialogue.text = interactSuc;
            play.Play();
            GetComponent<Collider2D>().enabled = false;
            SceneManager.LoadScene("FinalPuzzle");

        }
        else
        {
            DialogWindow.SetActive(true);
            textDialogue.text = interact;
        }
    }
}
