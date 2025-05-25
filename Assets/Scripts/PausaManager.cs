using UnityEngine;
using UnityEngine.EventSystems;

public class PausaManager : MonoBehaviour
{
    [Header("Panel de Pausa")]
    public GameObject panelPausa;

    [Header("Otras UIs a ocultar cuando pausas")]
    public GameObject[] otrasUIs;

    private bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            AlternarPausa();
    }

    void AlternarPausa()
{
    juegoPausado = !juegoPausado;

    if (panelPausa != null)
        panelPausa.SetActive(juegoPausado);

    foreach (var ui in otrasUIs)
        if (ui != null)
            ui.SetActive(!juegoPausado);

    Time.timeScale = juegoPausado ? 0f : 1f;

    Cursor.visible = juegoPausado;
    Cursor.lockState = juegoPausado
        ? CursorLockMode.None
        : CursorLockMode.Locked;

    if (juegoPausado)
    {
        StartCoroutine(SeleccionarBotonPorDefecto());
    }
    else
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}

private System.Collections.IEnumerator SeleccionarBotonPorDefecto()
{
    yield return null; 
    var primerBoton = panelPausa.GetComponentInChildren<UnityEngine.UI.Button>();
    if (primerBoton != null)
        EventSystem.current.SetSelectedGameObject(primerBoton.gameObject);
}

}
