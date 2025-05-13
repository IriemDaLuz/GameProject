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

        // ✅ No activamos canvasGroup ni textos aún. Se activan solo en ShowChapter()
        canvasGroup.alpha = 0f;

        // Desactiva canvas visual completo al inicio
        if (canvasGroup != null)
            canvasGroup.gameObject.SetActive(false);

        // Desactiva UI del juego al inicio
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
            Debug.LogError("❌ ChapterTitleManager está desactivado. ¡No se puede iniciar la secuencia!");
            return;
        }

        // Activamos todo para mostrar el título
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

        // Fade in
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(visibleDuration);

        // Fade out
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        // Desactivar visual completo del capítulo
        if (canvasGroup != null)
            canvasGroup.gameObject.SetActive(false);

        // Desactivar textos
        if (chapterNumberText != null)
            chapterNumberText.gameObject.SetActive(false);

        if (chapterNameText != null)
            chapterNameText.gameObject.SetActive(false);

        // Activar UI del juego al final
        foreach (GameObject canvas in canvasesDeJuego)
        {
            if (canvas != null)
                canvas.SetActive(true);
        }
    }
}
