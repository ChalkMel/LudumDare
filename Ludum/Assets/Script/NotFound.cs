using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NotFound : MonoBehaviour
{
    
     private GameObject DialogWindow;
     private TMP_Text textDialogue;
    [SerializeField] private string interact = "";
    private void Awake()
    {
        DialogWindow = GameObject.FindGameObjectWithTag("DialogWindow");
        textDialogue = DialogWindow.GetComponentInChildren<TMP_Text>();
    }
    private void OnMouseDown()
    {
        
        DialogWindow.SetActive(true);
        textDialogue.text = interact;
    }
}
