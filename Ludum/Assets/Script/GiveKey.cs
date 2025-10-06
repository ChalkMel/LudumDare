using UnityEngine;
using TMPro;

public class GiveKey : MonoBehaviour
{
    private GameObject DialogWindow;
    private TMP_Text textDialogue;
    [SerializeField] private string interact = "";
    private void Awake()
    {
        DialogWindow = GameObject.FindGameObjectWithTag("DialogWindow");
        textDialogue = DialogWindow.GetComponentInChildren<TMP_Text>();
    }
    [SerializeField] private GameObject key;
    private bool picked = false;
    private void OnMouseDown()
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
