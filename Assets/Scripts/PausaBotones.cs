using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaBotones : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject panelPausa;
    public GameObject[] otrasUIs;

    public void ReanudarJuego()
    {
        Time.timeScale = 1f;

        if (panelPausa != null)
            panelPausa.SetActive(false);

        foreach (var ui in otrasUIs)
            if (ui != null)
                ui.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal"); 
    }

    public void AbrirOpciones()
    {
        Debug.Log("Opciones aún no implementadas");
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public void MostrarAyuda()
    {
        Debug.Log("Mostrar panel de ayuda...");
        // Activa un panel de ayuda o instrucciones si lo tienes
    }
}
