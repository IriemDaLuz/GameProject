using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TriggerCapitulo3 : MonoBehaviour
{
    [Header("Zoom y cámara")]
    public Camera camara;
    public Transform puntoZoom;
    public float velocidadZoom = 2f;

    [Header("Audio")]
    public AudioSource audioFuente;
    public AudioClip sonidoMom;
    public AudioClip sonidoGolpe;

    [Header("Luz de la sala")]
    public Light luzSala;

    [Header("UIs a desactivar")]
    public GameObject[] otrasUIs;

    [Header("Jugador")]
    public GameObject jugador;
    public Transform posicionMorgue;          // ⬅️ Aquí irá el jugador al "despertar"
    public Transform camaraJugador;

    [Header("Desmayo visual")]
    public CanvasGroup fadeCanvas;

    private bool jugadorCerca = false;
    private bool secuenciaIniciada = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E) && !secuenciaIniciada)
        {
            StartCoroutine(SecuenciaDesmayoEnMorgue());
        }
    }

    private IEnumerator SecuenciaDesmayoEnMorgue()
    {
        secuenciaIniciada = true;

        foreach (var ui in otrasUIs)
            if (ui != null) ui.SetActive(false);

        // Zoom inicial
        Vector3 posInicial = camara.transform.position;
        Quaternion rotInicial = camara.transform.rotation;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * velocidadZoom;
            camara.transform.position = Vector3.Lerp(posInicial, puntoZoom.position, t);
            camara.transform.rotation = Quaternion.Slerp(rotInicial, puntoZoom.rotation, t);
            yield return null;
        }

        // Sonido ambiente
        if (audioFuente && sonidoMom)
            audioFuente.PlayOneShot(sonidoMom);

        yield return new WaitForSeconds(1.2f);

        if (luzSala != null)
            luzSala.enabled = false;

        // Sonido de golpe
        if (audioFuente && sonidoGolpe)
            audioFuente.PlayOneShot(sonidoGolpe);

        // Cámara "cae" con animación simple
        yield return StartCoroutine(AnimacionDesmayoCamara());

        // Fade a negro (ojos cerrándose)
        yield return StartCoroutine(FadeNegro(true));

// Teletransportar al jugador a la morgue
Debug.Log("Teletransportando a la morgue...");
CharacterController cc = jugador.GetComponent<CharacterController>();
if (cc != null) cc.enabled = false;

jugador.transform.position = posicionMorgue.position;
jugador.transform.rotation = posicionMorgue.rotation;

if (cc != null) cc.enabled = true;

// Resetear cámara
camaraJugador.localPosition = Vector3.zero;
camaraJugador.localRotation = Quaternion.identity;



        // Espera un momento en negro
        yield return new WaitForSeconds(1.2f);

        // Fade in (despertar)
        yield return StartCoroutine(FadeNegro(false));
    }

    private IEnumerator AnimacionDesmayoCamara()
    {
        if (camaraJugador == null) yield break;

        Vector3 posInicial = camaraJugador.localPosition;
        Quaternion rotInicial = camaraJugador.localRotation;

        Vector3 posFinal = posInicial + new Vector3(0f, -0.5f, 0.2f);
        Quaternion rotFinal = Quaternion.Euler(70f, 0f, 0f);

        float duracion = 1.2f;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;

            camaraJugador.localPosition = Vector3.Lerp(posInicial, posFinal, t);
            camaraJugador.localRotation = Quaternion.Slerp(rotInicial, rotFinal, t);

            yield return null;
        }
    }

    private IEnumerator FadeNegro(bool fadeIn)
    {
        if (fadeCanvas == null) yield break;

        float duracion = 1f;
        float tiempo = 0f;
        float inicio = fadeCanvas.alpha;
        float destino = fadeIn ? 1f : 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(inicio, destino, tiempo / duracion);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = false;
    }
}
