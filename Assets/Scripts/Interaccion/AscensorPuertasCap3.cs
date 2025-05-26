using UnityEngine;
using System.Collections;

public class AscensorPuertasCap3 : MonoBehaviour
{
    [Header("Referencias a Puertas")]
    public Transform puertaIzquierda;
    public Transform puertaDerecha;

    [Header("Movimiento de Puertas")]
    public Vector3 desplazamientoIzquierda = new Vector3(-1f, 0f, 0f);
    public Vector3 desplazamientoDerecha = new Vector3(1f, 0f, 0f);
    public float velocidad = 2f;

    private Vector3 posInicialIzq;
    private Vector3 posInicialDer;
    private bool abierta = false;

    void Start()
    {
        posInicialIzq = puertaIzquierda.localPosition;
        posInicialDer = puertaDerecha.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!abierta && other.CompareTag("Player") && ChapterManager.Instance.capituloActual == 3)
        {
            StartCoroutine(AbrirPuertas());
        }
    }

    private IEnumerator AbrirPuertas()
    {
        abierta = true;

        Vector3 destinoIzq = posInicialIzq + desplazamientoIzquierda;
        Vector3 destinoDer = posInicialDer + desplazamientoDerecha;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * velocidad;
            puertaIzquierda.localPosition = Vector3.Lerp(posInicialIzq, destinoIzq, t);
            puertaDerecha.localPosition = Vector3.Lerp(posInicialDer, destinoDer, t);
            yield return null;
        }
    }
}
