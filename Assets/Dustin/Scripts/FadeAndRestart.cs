using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeAndRestart : MonoBehaviour
{
    [Header("Assign the Canvas root")]
    public GameObject fadeRoot;
    public CanvasGroup fadeCanvas;
    public float fadeDuration = 1f;

    void Awake()
    {
        if (!fadeCanvas) fadeCanvas = GetComponentInChildren<CanvasGroup>(true);
        if (!fadeRoot && fadeCanvas) fadeRoot = fadeCanvas.GetComponentInParent<Canvas>(true)?.gameObject;
    }

    void Start()
    {
        if (fadeRoot) fadeRoot.SetActive(true);
        if (fadeCanvas)
        {
            fadeCanvas.alpha = 1f;
            StartCoroutine(FadeIn());
        }
    }

    public void TriggerFade()
    {
        if (fadeRoot) fadeRoot.SetActive(true);
        if (fadeCanvas) StartCoroutine(FadeOutAndRestart());
    }

    IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = 1f - Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }
        fadeCanvas.alpha = 0f;
    }

    IEnumerator FadeOutAndRestart()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
