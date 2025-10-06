using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterenceCutScene : MonoBehaviour
{
    private GameObject DialogWindow;
    private TMP_Text textDialogue;
    private void Awake()
    {
        DialogWindow = GameObject.FindGameObjectWithTag("DialogWindow");
        textDialogue = DialogWindow.GetComponentInChildren<TMP_Text>();
    }
    [SerializeField] private AudioSource cutscene;
    [SerializeField] private AudioSource cutscene2;
    [SerializeField] private Image panel;
    [SerializeField] private string interact = "Где я?.. Почему так пусто?.. Я не… не помню… Я… кто я?";
    void Start()
    {
        cutscene.Play();
        cutscene2.Play();
        StartCoroutine(Fade());
        DialogWindow.SetActive(true);
        textDialogue.text = interact;
    }

    System.Collections.IEnumerator Fade()
    {
        float alpha = 1f;

        while (alpha > 0)
        {
            while (alpha > 200)
            {
                alpha -= Time.deltaTime / 40;
            }
            alpha -= Time.deltaTime/10;
            panel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        if (alpha <=0)
        {
            Destroy(panel);
        }
    }

}
