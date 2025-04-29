using System.Collections;
using UnityEngine;
using TMPro;

public class ChapterTitleManager : MonoBehaviour
{
    public static ChapterTitleManager Instance;

    public CanvasGroup canvasGroup;
    public TMP_Text chapterNumberText;
    public TMP_Text chapterNameText;  
    public float fadeDuration = 1.5f;
    public float visibleDuration = 3f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    public void ShowChapter(string chapterNumber, string chapterName)
    {
        if (chapterNumberText != null)
            chapterNumberText.text = chapterNumber;
        
        if (chapterNameText != null)
            chapterNameText.text = chapterName;

        gameObject.SetActive(true);
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        canvasGroup.alpha = 0f;

        float t = 0f;
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

        gameObject.SetActive(false);
    }
}
