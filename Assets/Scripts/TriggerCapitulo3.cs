using UnityEngine;
using TMPro;
using System.Collections;

public class TriggerCapitulo3 : MonoBehaviour
{
    [Header("Zoom y cámara")]
    public Camera camara;
    public Transform puntoZoom;
    public float velocidadZoom = 2f;

    [Header("Audio")]
    public AudioSource audioFuente;
    public AudioClip sonidoMom;

    [Header("Luz de la sala a apagar")]
    public GameObject luzSala;

    [Header("Sombra simbólica")]
    public GameObject sombraVisual;

    [Header("Texto de interacción")]
    public TMP_Text textoInteractuar;

    [Header("UIs a desactivar")]
    public GameObject[] otrasUIs;

    private bool jugadorCerca = false;
    private bool secuenciaIniciada = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E) && !secuenciaIniciada)
        {
            StartCoroutine(SecuenciaCapitulo3());
            if (textoInteractuar != null)
                textoInteractuar.gameObject.SetActive(false);
        }
    }

    private IEnumerator SecuenciaCapitulo3()
    {
        secuenciaIniciada = true;

        foreach (var ui in otrasUIs)
            if (ui != null) ui.SetActive(false);

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

        if (audioFuente != null && sonidoMom != null)
            audioFuente.PlayOneShot(sonidoMom);

        yield return new WaitForSeconds(2f);
        
        if (luzSala != null)
            luzSala.SetActive(false);

        if (sombraVisual != null)
            sombraVisual.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        if (ChapterManager.Instance != null)
            ChapterManager.Instance.CambiarCapitulo(3);

        yield return new WaitForSeconds(4f);

        float z = 0f;
        while (z < 1f)
        {
            z += Time.deltaTime * velocidadZoom;
            camara.transform.position = Vector3.Lerp(puntoZoom.position, posInicial, z);
            camara.transform.rotation = Quaternion.Slerp(puntoZoom.rotation, rotInicial, z);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            if (textoInteractuar != null && !secuenciaIniciada)
            {
                textoInteractuar.text = "Pulsa [E] para asomarte";
                textoInteractuar.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            if (textoInteractuar != null)
                textoInteractuar.gameObject.SetActive(false);
        }
    }
}
