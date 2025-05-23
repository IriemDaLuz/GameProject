using UnityEngine;
using System.Collections;
using TMPro;

public class DoubleDoorWithKey : MonoBehaviour
{
    public string requiredItemID = "pase_medico";

    [Header("Puertas")]
    public Transform puertaIzquierda;
    public Transform puertaDerecha;

    [Header("Rotación")]
    public Vector3 rotacionIzquierda = new Vector3(0, 90, 0);
    public Vector3 rotacionDerecha = new Vector3(0, -90, 0);
    public float velocidad = 2f;

    [Header("UI")]
    public TMP_Text mensajeUI;
    public TMP_Text textoInteractuar;
    public float duracionMensaje = 2f;

    private bool abierta = false;
    private bool playerCerca = false;

    private void Update()
    {
        if (playerCerca && Input.GetKeyDown(KeyCode.E) && !abierta)
        {
            if (textoInteractuar != null)
                textoInteractuar.gameObject.SetActive(false); 

            if (!MissionManager.Instance.misionElectricidadCompletada)
            {
                MostrarMensaje("Necesitas restablecer la electricidad.");
                return;
            }

            var hand = HandSystem.Instance;

            bool tienePase = (hand.leftHandItem != null && hand.leftHandItem.id == requiredItemID)
                          || (hand.rightHandItem != null && hand.rightHandItem.id == requiredItemID);

            if (!tienePase)
            {
                MostrarMensaje("Necesitas el pase médico.");
                return;
            }

            bool paseAsignado = (hand.leftHandItem != null && hand.leftHandItem.id == requiredItemID)
                             || (hand.rightHandItem != null && hand.rightHandItem.id == requiredItemID);

            if (!paseAsignado)
            {
                MostrarMensaje("Asigna el pase médico a una mano.");
                return;
            }

            StartCoroutine(AbrirPuertas());
            abierta = true;
        }
    }

    private IEnumerator AbrirPuertas()
    {
        Quaternion inicioIzq = puertaIzquierda.localRotation;
        Quaternion destinoIzq = Quaternion.Euler(rotacionIzquierda);

        Quaternion inicioDer = puertaDerecha.localRotation;
        Quaternion destinoDer = Quaternion.Euler(rotacionDerecha);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * velocidad;
            puertaIzquierda.localRotation = Quaternion.Slerp(inicioIzq, destinoIzq, t);
            puertaDerecha.localRotation = Quaternion.Slerp(inicioDer, destinoDer, t);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = true;
            if (textoInteractuar != null && !abierta)
                textoInteractuar.text = "Pulsa 'E' para interactuar";
                textoInteractuar.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = false;
            if (textoInteractuar != null)
                textoInteractuar.gameObject.SetActive(false);
        }
    }

    private void MostrarMensaje(string texto)
    {
        if (mensajeUI != null)
        {
            mensajeUI.text = texto;
            CancelInvoke(nameof(LimpiarMensaje));
            Invoke(nameof(LimpiarMensaje), duracionMensaje);
        }
    }

    private void LimpiarMensaje()
    {
        if (mensajeUI != null)
            mensajeUI.text = "";
    }
}
