using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinalDecisionManager : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup canvasUI;
    public TMP_Text textoParca;
    public Button botonHijo;
    public Button botonTu;

    [Header("Jugador y Cámara")]
    public GameObject jugador;
    public Transform camaraJugador;

    [Header("Parca")]
    public GameObject parca;

    [Header("Transición")]
    public CanvasGroup fadeCanvas;
    public float duracionFade = 1.5f;
    public string nombreEscenaCreditos = "Creditos";

    private bool finalIniciado = false;

    void Start()
    {
        // Escucha cuando cambie el capítulo
        ChapterManager.OnCapituloCambiado += RevisarActivacionFinal;
    }

    void OnDestroy()
    {
        ChapterManager.OnCapituloCambiado -= RevisarActivacionFinal;
    }

    void RevisarActivacionFinal(int capitulo)
    {
        if (capitulo == 3)
        {
            StartCoroutine(EsperarAntesDeIniciarFinal());
        }
    }

    IEnumerator EsperarAntesDeIniciarFinal()
    {
        // Espera que el jugador se teletransporte antes de activar el final
        yield return new WaitForSeconds(1.5f);
        IniciarFinal();
    }

    public void IniciarFinal()
    {
        if (!finalIniciado)
        {
            finalIniciado = true;
            StartCoroutine(EscenaFinal());
        }
    }

    IEnumerator EscenaFinal()
    {
        BloquearJugador();

        // Fade in desde negro (si está negro)
        if (fadeCanvas) fadeCanvas.alpha = 1f;
        yield return StartCoroutine(Fade(0f));
        // Activa el objeto Canvas si estaba desactivado
canvasUI.gameObject.SetActive(true);

// Luego lo hace visible
canvasUI.alpha = 1f;
textoParca.text = "¿Tantas ansias por descubrir la verdad?";


        // Aparece la parca
        if (parca) parca.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        // Mostrar primer mensaje
        textoParca.text = "¿Tantas ansias por descubrir la verdad?";
        canvasUI.alpha = 1f;

        yield return new WaitForSeconds(3f);

        // Mensaje final + opciones
        textoParca.text = "Ahora decide qué quieres hacer:\n¿Salvar a tu hijo o salvarte a ti?";

        botonHijo.gameObject.SetActive(true);
        botonTu.gameObject.SetActive(true);

        botonHijo.onClick.AddListener(() => ElegirFinal("hijo"));
        botonTu.onClick.AddListener(() => ElegirFinal("yo"));
    }

    void ElegirFinal(string eleccion)
    {
        Debug.Log($"Elección: {eleccion}");
        StartCoroutine(FinalizarJuego());
    }

    IEnumerator FinalizarJuego()
    {
        yield return StartCoroutine(Fade(1f));
        SceneManager.LoadScene(nombreEscenaCreditos);
    }

    IEnumerator Fade(float alphaFinal)
    {
        float t = 0f;
        float alphaInicio = fadeCanvas.alpha;

        while (t < duracionFade)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(alphaInicio, alphaFinal, t / duracionFade);
            yield return null;
        }
    }

    void BloquearJugador()
    {
        // Desactiva movimiento
        var cc = jugador.GetComponent<CharacterController>();
        if (cc) cc.enabled = false;

        var mover = jugador.GetComponent<PlayerMovement>();
        if (mover) mover.controlesActivos = false;

        // Reset cámara
        if (camaraJugador)
        {
            camaraJugador.localPosition = Vector3.zero;
            camaraJugador.localRotation = Quaternion.identity;
        }

        // Mostrar cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
