using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovimientoPersonaje : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento = 2f;
    [SerializeField] private float sensibilidadCamara = 1.5f;

    [Header("Referencias")]
    [SerializeField] private Transform transformPersonaje;
    [SerializeField] private Camera camaraPersonaje;

    private CharacterController characterController;
    private Vector3 direccionMovimiento;
    private float rotacionX;
    private bool interactuandoConUI = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        ManejarCursor();
    }

    private void Update()
    {
        if (!interactuandoConUI)
        {
            MovimientoDelPersonaje();
            MovimientoDeCamara();
        }

        ManejarCursor();
    }

    private void ManejarCursor()
    {
        Cursor.visible = interactuandoConUI;
        Cursor.lockState = interactuandoConUI ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void MovimientoDelPersonaje()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        direccionMovimiento = (transformPersonaje.right * inputX + transformPersonaje.forward * inputZ).normalized;
        characterController.SimpleMove(direccionMovimiento * velocidadMovimiento);
    }

    private void MovimientoDeCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadCamara;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadCamara;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -80f, 80f);

        camaraPersonaje.transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
        transformPersonaje.Rotate(Vector3.up * mouseX);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UIInteractiva"))
            interactuandoConUI = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("UIInteractiva"))
            interactuandoConUI = false;
    }

    private void OnGUI()
    {
        if (!interactuandoConUI)
        {
            float crosshairSize = 6f;
            GUI.color = new Color(1, 0, 0, 0.5f);
            GUI.DrawTexture(
                new Rect(Screen.width / 2 - crosshairSize / 2, Screen.height / 2 - crosshairSize / 2, crosshairSize, crosshairSize),
                Texture2D.whiteTexture
            );
        }
    }
}
