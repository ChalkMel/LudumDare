using UnityEngine;

public class Darkness : MonoBehaviour
{
    public AudioClip whisperSound;
    public GameObject darknessPanel;
    private AudioSource audioSource;
    private bool hasPlayed = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = whisperSound;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
                audioSource.Play();
                hasPlayed = true;
                darknessPanel.SetActive(true);
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            darknessPanel.SetActive(false);
            hasPlayed = false;
            audioSource.Stop();

        }
    }
}
