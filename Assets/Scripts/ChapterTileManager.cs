using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChapterTitleManager : MonoBehaviour
{
    public static ChapterTitleManager Instance;

    [Header("UI del Capítulo")]
    public CanvasGroup canvasGroup;
    public TMP_Text chapterNumberText;
    public TMP_Text chapterNameText;

    [Header("Duraciones")]
    public float fadeDuration = 1.5f;
    public float visibleDuration = 3f;

    [Header("Canvas que deben activarse después")]
    public List<GameObject> canvasesDeJuego = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        canvasGroup.alpha = 0f;

        if (canvasGroup != null)
            canvasGroup.gameObject.SetActive(false);

        foreach (GameObject canvas in canvasesDeJuego)
        {
            if (canvas != null)
                canvas.SetActive(false);
        }
    }

    public void ShowChapter(string chapterNumber, string chapterName)
    {
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogError(" ChapterTitleManager está desactivado. ");
            return;
        }

        if (canvasGroup != null)
            canvasGroup.gameObject.SetActive(true);

        if (chapterNumberText != null)
        {
            chapterNumberText.gameObject.SetActive(true);
            chapterNumberText.text = chapterNumber;
        }

        if (chapterNameText != null)
        {
            chapterNameText.gameObject.SetActive(true);
            chapterNameText.text = chapterName;
        }

        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        float t = 0f;
        canvasGroup.alpha = 0f;
        
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(visibleDuration);

        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        if (canvasGroup != null)
            canvasGroup.gameObject.SetActive(false);

        if (chapterNumberText != null)
            chapterNumberText.gameObject.SetActive(false);

        if (chapterNameText != null)
            chapterNameText.gameObject.SetActive(false);

        foreach (GameObject canvas in canvasesDeJuego)
        {
            if (canvas != null)
                canvas.SetActive(true);
        }
    }
}
