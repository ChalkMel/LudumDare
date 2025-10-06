using TMPro;
using UnityEngine;

public class CountUI : MonoBehaviour
{
    private GameObject DialogWindow;
    private TMP_Text textDialogue;
    [SerializeField] private string interact = "";
    [SerializeField] public int countClues = 5;
    [SerializeField] public TMP_Text counter;
    [SerializeField] private GameObject countUI;
    private void Awake()
    {
        DialogWindow = GameObject.FindGameObjectWithTag("DialogWindow");
        textDialogue = DialogWindow.GetComponentInChildren<TMP_Text>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DialogWindow.SetActive(true);
        textDialogue.text = interact;
        countUI.SetActive(true);
        
    }
    private void Update()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();
    }
}
