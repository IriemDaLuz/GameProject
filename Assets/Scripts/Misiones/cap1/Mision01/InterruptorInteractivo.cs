using UnityEngine;

public class InterruptorInteractivo : MonoBehaviour
{
    public enum Estado { OFF, ON, BLOQUEADO }
    public Estado estadoActual = Estado.OFF;

    [Header("Materiales para la luz")]
    public Renderer luzRenderer;
    public Material materialOFF;
    public Material materialON;
    public Material materialBloqueado;

    [Header("Transform del interruptor")]
    public Vector3 posOFF;
    public Vector3 rotOFF;
    public Vector3 posON;
    public Vector3 rotON;
    public Vector3 posBloqueado;
    public Vector3 rotBloqueado;

    [Header("Transform de la luz")]
    public Transform luzTransform;
    public Vector3 luzPosOFF;
    public Vector3 luzPosON;
    public Vector3 luzPosBloqueado;

    private float holdTime = 2f;
    private float mouseDownTime;

    void Start()
    {
        AplicarEstadoVisual();
    }

    private void OnMouseDown()
    {
        mouseDownTime = Time.time;
    }

    private void OnMouseUp()
    {
        float heldDuration = Time.time - mouseDownTime;

        if (estadoActual == Estado.BLOQUEADO)
        {
            estadoActual = Estado.OFF;
        }
        else if (heldDuration >= holdTime)
        {
            estadoActual = Estado.BLOQUEADO;
        }
        else
        {
            estadoActual = (estadoActual == Estado.OFF) ? Estado.ON : Estado.OFF;
        }

        AplicarEstadoVisual();

        var puzzle = Object.FindFirstObjectByType<PuzzleElectricidad>();
        if (puzzle != null) puzzle.VerificarCombinacion();
    }

    void AplicarEstadoVisual()
    {
        // Posición y rotación del interruptor
        switch (estadoActual)
        {
            case Estado.ON:
                transform.localPosition = posON;
                transform.localRotation = Quaternion.Euler(rotON);
                break;
            case Estado.OFF:
                transform.localPosition = posOFF;
                transform.localRotation = Quaternion.Euler(rotOFF);
                break;
            case Estado.BLOQUEADO:
                transform.localPosition = posBloqueado;
                transform.localRotation = Quaternion.Euler(rotBloqueado);
                break;
        }

        // Color de la luz
        if (luzRenderer != null)
        {
            luzRenderer.material = estadoActual switch
            {
                Estado.ON => materialON,
                Estado.OFF => materialOFF,
                Estado.BLOQUEADO => materialBloqueado,
                _ => luzRenderer.material
            };
        }

        // Posición de la luz
        if (luzTransform != null)
        {
            switch (estadoActual)
            {
                case Estado.ON:
                    luzTransform.localPosition = luzPosON;
                    break;
                case Estado.OFF:
                    luzTransform.localPosition = luzPosOFF;
                    break;
                case Estado.BLOQUEADO:
                    luzTransform.localPosition = luzPosBloqueado;
                    break;
            }
        }
    }
}