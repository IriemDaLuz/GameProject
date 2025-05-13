using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class SistemaInspeccion : MonoBehaviour
{
    [Header("Cámara")]
    public Camera camaraInspeccion;

    [Header("Opciones")]
    public float distancia = 2f;
    public LayerMask capaObjetos;

    [Header("UI del juego")]
    public GameObject canvasUIPrincipal;

    [Header("UI de Inspección")]
    public GameObject panelTextoUI;
    public GameObject canvasInspeccion;
    public Transform puntoVisual;
    public TMP_Text textoNombre;
    public TMP_Text textoDescripcion;

    [Header("Lectura extendida")]
    public GameObject panelLectura;
    public TMP_Text textoLectura;
    public Button botonAnterior;
    public Button botonSiguiente;

    private ObjetoInspeccionable objetoActual;
    private GameObject modeloInstanciado;
    private bool inspeccionando = false;

    private List<string> paginas = new List<string>();
    private int paginaActual = 0;
    private int caracteresPorPagina = 800;

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

    public void IniciarInspeccion()
    {
        if (objetoActual == null) return;

        inspeccionando = true;

        if (canvasUIPrincipal != null)
            canvasUIPrincipal.SetActive(false);

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

        PausarJuego(true);
    }

    public void MostrarLecturaCompleta()
    {
        if (panelLectura != null)
            panelLectura.SetActive(true);

        if (textoDescripcion != null)
            textoDescripcion.gameObject.SetActive(false);

        paginas.Clear();
        string desc = objetoActual.textoLecturaCompleta;
        for (int i = 0; i < desc.Length; i += caracteresPorPagina)
        {
            int length = Mathf.Min(caracteresPorPagina, desc.Length - i);
            paginas.Add(desc.Substring(i, length));
        }

        paginaActual = 0;
        ActualizarPagina();
    }

    public void CambiarPagina(int direccion)
    {
        paginaActual += direccion;
        paginaActual = Mathf.Clamp(paginaActual, 0, paginas.Count - 1);
        ActualizarPagina();
    }

    private void ActualizarPagina()
    {
        textoLectura.text = paginas[paginaActual];
        botonAnterior.interactable = paginaActual > 0;
        botonSiguiente.interactable = paginaActual < paginas.Count - 1;
    }

    public void CerrarInspeccion()
    {
        inspeccionando = false;

        if (canvasUIPrincipal != null)
            canvasUIPrincipal.SetActive(true);

        if (canvasInspeccion != null)
            canvasInspeccion.SetActive(false);

        if (panelLectura != null)
            panelLectura.SetActive(false);

        if (textoDescripcion != null)
            textoDescripcion.gameObject.SetActive(true);

        if (modeloInstanciado != null)
            Destroy(modeloInstanciado);

        PausarJuego(false);
    }

    private void PausarJuego(bool pausar)
    {
        if (pausar)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }
}
