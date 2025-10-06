using UnityEngine;
using UnityEngine.UI;

public class DialogueRemove : MonoBehaviour
{
    [SerializeField] private GameObject Dialogue;
    [SerializeField] private Button DialogueBut;
    private void Awake()
    {
        DialogueBut.onClick.AddListener(Remove);
    }
    private void Remove()
    {
        Dialogue.SetActive(false);
    }
}