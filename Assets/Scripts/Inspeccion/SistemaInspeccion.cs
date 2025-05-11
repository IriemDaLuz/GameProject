using UnityEngine;
using TMPro;

public class SistemaInspeccion : MonoBehaviour
{
    public float distancia = 2f;
    public LayerMask capaObjetos;
    public GameObject panelInteraccionUI;
    public GameObject canvasInspeccion;
    public Transform puntoVisual;
    public TMP_Text nombreTexto;
    public TMP_Text descripcionTexto;

    private GameObject objetoActual;
    private GameObject modeloInstanciado;

    void Update()
    {
        if (canvasInspeccion.activeSelf) return;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distancia, capaObjetos))
        {
            if (hit.collider.TryGetComponent(out ObjetoInspeccionable obj))
            {
                panelInteraccionUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    ActivarInspeccion(obj);
                }

                return;
            }
        }

        panelInteraccionUI.SetActive(false);
    }

    void ActivarInspeccion(ObjetoInspeccionable obj)
    {
        canvasInspeccion.SetActive(true);
        nombreTexto.text = obj.nombreObjeto;
        descripcionTexto.text = obj.descripcionObjeto;

        modeloInstanciado = Instantiate(obj.modeloParaInspeccionar, puntoVisual.position, Quaternion.identity, puntoVisual);
        modeloInstanciado.AddComponent<RotadorDeObjeto>();
    }

    public void CerrarInspeccion()
    {
        Destroy(modeloInstanciado);
        canvasInspeccion.SetActive(false);
    }
}
