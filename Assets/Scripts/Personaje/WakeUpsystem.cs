using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WakeUpsystem : MonoBehaviour
{
    [Header("Referencias")]
    public Transform camaraJugador;
    public GameObject controladorJugador;
    public AudioSource fuenteAudio;
    public AudioClip clipVozSatan;
    public CanvasGroup canvasFade;
    public GameObject canvasUIPrincipal;
    public GameObject canvasUIExtra;
    public MonoBehaviour scriptMovimientoJugador;

    [Header("Texto y Subtítulos")]
    public TMP_Text textoIndicacion;
    public TMP_Text textoSubtitulos;

    [Header("UI de Salto")]
    public TMP_Text textoSaltarIntro;

    [Header("Subtítulos")]
    public float[] tiemposSubtitulos;
    public string[] lineasSubtitulos;

    private Vector3 posicionInicialCamara;
    private Quaternion rotacionInicialCamara;
    private bool listoParaDespertar = false;
    private bool haDespertado = false;

    void Start()
    {
        posicionInicialCamara = camaraJugador.localPosition;
        rotacionInicialCamara = camaraJugador.localRotation;

        textoIndicacion.text = "";
        textoSubtitulos.text = "";

        if (canvasUIPrincipal != null)
            canvasUIPrincipal.SetActive(false);

        if (canvasUIExtra != null)
            canvasUIExtra.SetActive(false);

        if (scriptMovimientoJugador != null)
            scriptMovimientoJugador.enabled = false;

        if (textoSaltarIntro != null)
        {
            textoSaltarIntro.gameObject.SetActive(true);
            textoSaltarIntro.text = "Presiona [Espacio] para saltar introducción";
        }

        StartCoroutine(SecuenciaIntro());
    }

    void Update()
    {
        if (listoParaDespertar && !haDespertado && Input.GetKeyDown(KeyCode.E))
        {
            haDespertado = true;
            StartCoroutine(SecuenciaDespertar());
        }

        if (!haDespertado && Input.GetKeyDown(KeyCode.Space))
        {
            SkipIntro();
        }
    }

    IEnumerator SecuenciaIntro()
    {
        yield return StartCoroutine(MirarIzquierda());
        yield return StartCoroutine(Parpadeo());
        yield return StartCoroutine(MirarDerecha());
        yield return StartCoroutine(Parpadeo());

        camaraJugador.localRotation = Quaternion.Euler(0f, 0f, 0f);
        canvasFade.alpha = 0f;

        ReproducirVoz();
        StartCoroutine(SecuenciaSubtitulos());

        yield return new WaitForSeconds(clipVozSatan.length + 1f);

        if (ChapterTitleManager.Instance != null)
        {
            ChapterTitleManager.Instance.ShowChapter("Capítulo 1", "El Despertar");
        }

        yield return new WaitForSeconds(6f);

        if (textoIndicacion != null)
        {
            textoIndicacion.gameObject.SetActive(true);
            textoIndicacion.text = "Presiona [E] para levantarte";
        }

        listoParaDespertar = true;
    }

    void ReproducirVoz()
    {
        if (!fuenteAudio.isPlaying)
        {
            fuenteAudio.clip = clipVozSatan;
            fuenteAudio.Play();
        }
    }

    IEnumerator Parpadeo()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 4f;
            canvasFade.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 4f;
            canvasFade.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }
    }

    IEnumerator ParpadeoFinal()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 4f;
            canvasFade.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 4f;
            canvasFade.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }
    }

    IEnumerator MirarIzquierda()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 0.5f;
            camaraJugador.localRotation = Quaternion.Slerp(rotacionInicialCamara, Quaternion.Euler(0f, -30f, 0f), t);
            yield return null;
        }
    }

    IEnumerator MirarDerecha()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 0.5f;
            camaraJugador.localRotation = Quaternion.Slerp(Quaternion.Euler(0f, -30f, 0f), Quaternion.Euler(0f, 30f, 0f), t);
            yield return null;
        }
    }

    IEnumerator SecuenciaSubtitulos()
    {
        int index = 0;
        while (index < lineasSubtitulos.Length)
        {
            yield return new WaitForSeconds(tiemposSubtitulos[index]);
            textoSubtitulos.text = lineasSubtitulos[index];
            index++;
        }
        yield return new WaitForSeconds(2f);
        textoSubtitulos.text = "";
    }

    IEnumerator SecuenciaDespertar()
    {
        textoIndicacion.text = "";

        if (textoSaltarIntro != null)
            textoSaltarIntro.gameObject.SetActive(false);

        yield return StartCoroutine(ParpadeoFinal());

        float elapsed = 0f;
        float duracion = 3f;

        Vector3 posInicial = camaraJugador.localPosition;
        Quaternion rotInicial = camaraJugador.localRotation;

        Vector3 posMedia = posInicial + new Vector3(-0.2f, 0f, 0.2f);
        Quaternion rotMedia = Quaternion.Euler(20f, -30f, 0f);

        Vector3 posFinal = posInicial + new Vector3(0f, 1.2f, 0f);
        Quaternion rotFinal = Quaternion.Euler(0f, 0f, 0f);

        while (elapsed < duracion / 2)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (duracion / 2);

            camaraJugador.localPosition = Vector3.Lerp(posInicial, posMedia, t);
            camaraJugador.localRotation = Quaternion.Slerp(rotInicial, rotMedia, t);

            yield return null;
        }

        elapsed = 0f;
        while (elapsed < duracion / 2)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (duracion / 2);

            camaraJugador.localPosition = Vector3.Lerp(posMedia, posFinal, t);
            camaraJugador.localRotation = Quaternion.Slerp(rotMedia, rotFinal, t);

            yield return null;
        }

        yield return StartCoroutine(BajarDeCamilla());

        if (canvasUIPrincipal != null)
            canvasUIPrincipal.SetActive(true);

        if (canvasUIExtra != null)
            canvasUIExtra.SetActive(true);

        if (scriptMovimientoJugador != null)
            scriptMovimientoJugador.enabled = true;

        StartCoroutine(MareoDespuesDespertar());
        gameObject.SetActive(false);
    }

    IEnumerator BajarDeCamilla()
    {
        float elapsed = 0f;
        float duracion = 1.2f;

        Vector3 posInicio = camaraJugador.localPosition;
        Vector3 posDestino = posInicio + new Vector3(0.4f, -1.0f, 0f);

        while (elapsed < duracion)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duracion;

            camaraJugador.localPosition = Vector3.Lerp(posInicio, posDestino, t);
            yield return null;
        }
    }

    IEnumerator MareoDespuesDespertar()
    {
        float timer = 0f;
        float duracion = 5f;

        while (timer < duracion)
        {
            timer += Time.deltaTime;
            float swayX = Mathf.Sin(timer * 1.5f) * 2f;
            float swayY = Mathf.Cos(timer * 1.2f) * 1.5f;

            camaraJugador.localRotation = Quaternion.Euler(swayY, swayX, 0f);
            yield return null;
        }

        camaraJugador.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void SkipIntro()
    {
        StopAllCoroutines();

        if (fuenteAudio != null && fuenteAudio.isPlaying)
            fuenteAudio.Stop();

        if (textoIndicacion != null)
            textoIndicacion.text = "";

        if (textoSubtitulos != null)
            textoSubtitulos.text = "";

        if (textoSaltarIntro != null)
            textoSaltarIntro.gameObject.SetActive(false);

        if (canvasFade != null)
            canvasFade.alpha = 0f;

        haDespertado = true;
        listoParaDespertar = false;

        StartCoroutine(SecuenciaDespertar());
    }
}
