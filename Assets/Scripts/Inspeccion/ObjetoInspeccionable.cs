using UnityEngine;

public class ObjetoInspeccionable : MonoBehaviour
{
    [Header("Datos del objeto")]
    public string nombreObjeto;

    [TextArea(3, 10)]
    public string descripcion;

    [TextArea(5, 20)]
    public string textoLecturaCompleta;

    [Header("Modelo (ya no se usa)")]
    public GameObject prefabModelo;

    [Header("Vista previa del objeto")]
    public Sprite imagenObjeto;
}
