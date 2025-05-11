using UnityEngine;

public class ObjetoInspeccionable : MonoBehaviour
{
    public string nombreObjeto;
    [TextArea(3,10)]
    public string descripcionObjeto;
    public GameObject modeloParaInspeccionar;
}
