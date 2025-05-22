using UnityEngine;

using System.Collections;
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

    private bool abierta = false;
    private bool playerCerca = false;

    private void Update()
    {
        if (playerCerca && Input.GetKeyDown(KeyCode.E) && !abierta)
        {
            if (!MissionManager.Instance.misionElectricidadCompletada)
            {
                Debug.Log("No hay electricidad.");
                return;
            }

            if (TienePaseMedico())
            {
                StartCoroutine(AbrirPuertas());
                abierta = true;
            }
            else
            {
                Debug.Log("Necesitas el pase médico en la mano.");
            }
        }
    }

    private bool TienePaseMedico()
    {
        var hand = HandSystem.Instance;
        return (hand.leftHandItem != null && hand.leftHandItem.id == requiredItemID)
            || (hand.rightHandItem != null && hand.rightHandItem.id == requiredItemID);
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
            playerCerca = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerCerca = false;
    }
}
