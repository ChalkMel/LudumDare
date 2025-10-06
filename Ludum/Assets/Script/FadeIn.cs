using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] private Button finaleButton;
    [SerializeField] private GameObject image2;
    [SerializeField] private GameObject finale;
    void Start()
    {
        StartCoroutine(Fade());
    }
    System.Collections.IEnumerator Fade()
    {
        float alpha = 0f;

        while (alpha < 255)
        {
            alpha += Time.deltaTime;
            image.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        if (alpha == 255)
        {
            finale.SetActive(true);
            finaleButton.onClick.AddListener(End2);
        }
    }
    public void End2()
    {
        SceneManager.LoadScene("Menu");
    }
}
