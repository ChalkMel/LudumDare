using TMPro;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    private GameObject DialogWindow;
    private TMP_Text textDialogue;

    public AudioClip whisperSound;
    public GameObject darknessPanel;
    public GameObject darknesscollider;
    private AudioSource audioSource;
    private bool hasPlayed = false;
    [SerializeField] private string interact = "";

    void Start()
    {
        DialogWindow = GameObject.FindGameObjectWithTag("DialogWindow");
        textDialogue = DialogWindow.GetComponentInChildren<TMP_Text>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = whisperSound;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            Inventory inventory = FindFirstObjectByType<Inventory>();
            if (inventory.HasItem("Bunny") && inventory.GetSelectedItem() == "Bunny")
            {

                    inventory.UseItem("Bunny");
                    darknesscollider.SetActive(false);
            }
            else
            {
                audioSource.Play();
                hasPlayed = true;
                darknessPanel.SetActive(true);
                DialogWindow.SetActive(true);
                textDialogue.text = interact;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (inventory.HasItem("Bunny") && inventory.GetSelectedItem() == "Bunny")
        {

            inventory.UseItem("Bunny");
            darknesscollider.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            darknessPanel.SetActive(false);
            hasPlayed = false;
            audioSource.Stop();
            DialogWindow.SetActive(false);
        }
    }
}
