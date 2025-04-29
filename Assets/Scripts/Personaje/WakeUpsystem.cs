using UnityEngine;
using System.Collections;

public class WakeUpSystem : MonoBehaviour
{
    public Transform playerCamera; // Asigna aquí la MainCamera
    public float wakeUpDuration = 3f;
    public GameObject playerController; // El objeto que controlará el movimiento después

    private Vector3 startCamPosition;
    private Quaternion startCamRotation;
    private Vector3 endCamPosition;
    private Quaternion endCamRotation;

    void Start()
    {
        // Guardamos la posición y rotación inicial
        startCamPosition = playerCamera.localPosition;
        startCamRotation = playerCamera.localRotation;

        // Definimos hacia dónde queremos movernos (levantar)
        endCamPosition = startCamPosition + new Vector3(0f, 1.2f, 0f); // Se levanta
        endCamRotation = Quaternion.Euler(0f, 0f, 0f);

        // Iniciamos la secuencia
        StartCoroutine(WakeUpSequence());
    }

    IEnumerator WakeUpSequence()
    {
        float elapsed = 0f;

        while (elapsed < wakeUpDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / wakeUpDuration;

            playerCamera.localPosition = Vector3.Lerp(startCamPosition, endCamPosition, t);
            playerCamera.localRotation = Quaternion.Slerp(startCamRotation, endCamRotation, t);

            yield return null;
        }

        EnablePlayerMovement();
    }

    void EnablePlayerMovement()
    {
        playerController.SetActive(true); // Activamos el movimiento normal
    }
}
