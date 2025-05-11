using UnityEngine;

public class ObjetoInspeccionable : MonoBehaviour
{
    public string nombreObjeto;
    
    [TextArea(3, 10)]
    public string descripcion;

    public GameObject prefabModelo;  
}
