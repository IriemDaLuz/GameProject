using UnityEngine;
using TMPro;

public class SistemaInspeccion : MonoBehaviour
{
    [Header("Cámara de Raycast")]
    public Camera camaraInspeccion;

    [Header("Opciones")]
    public float distancia = 2f;
    public LayerMask capaObjetos;

    [Header("UI")]
    public GameObject panelTextoUI;
    public GameObject canvasInspeccion;
    public Transform puntoVisual;
    public TMP_Text textoNombre;
    public TMP_Text textoDescripcion;

    private ObjetoInspeccionable objetoActual;
    private GameObject modeloInstanciado;
    private bool inspeccionando = false;

    void Update()
    {
        if (inspeccionando || camaraInspeccion == null) return;

        Ray ray = new Ray(camaraInspeccion.transform.position, camaraInspeccion.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distancia, capaObjetos))
        {
            objetoActual = hit.collider.GetComponent<ObjetoInspeccionable>();
            if (objetoActual != null)
            {
                if (panelTextoUI != null && !panelTextoUI.activeSelf)
                    panelTextoUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                    IniciarInspeccion();

                return;
            }
        }

        objetoActual = null;
        if (panelTextoUI != null && panelTextoUI.activeSelf)
            panelTextoUI.SetActive(false);
    }

    void IniciarInspeccion()
    {
        if (objetoActual == null) return;

        inspeccionando = true;
        if (canvasInspeccion != null)
            canvasInspeccion.SetActive(true);

        if (textoNombre != null)
            textoNombre.text = objetoActual.nombreObjeto;

        if (textoDescripcion != null)
            textoDescripcion.text = objetoActual.descripcion;

        if (objetoActual.prefabModelo != null && puntoVisual != null)
        {
            modeloInstanciado = Instantiate(objetoActual.prefabModelo, puntoVisual.position, Quaternion.identity, puntoVisual);
            modeloInstanciado.AddComponent<RotadorDeObjeto>();
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CerrarInspeccion()
    {
        inspeccionando = false;

        if (canvasInspeccion != null)
            canvasInspeccion.SetActive(false);

        if (modeloInstanciado != null)
            Destroy(modeloInstanciado);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
