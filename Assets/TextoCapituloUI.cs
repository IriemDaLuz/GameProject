using UnityEngine;
using TMPro;

public class TextoCapituloUI : MonoBehaviour
{
    [Header("Texto donde se mostrará el capítulo")]
    public TMP_Text textoCapitulo;

    void Start()
    {
        if (textoCapitulo == null)
        {
            Debug.LogError("TextoCapituloUI: No se asignó el TMP_Text.");
            return;
        }

        // Mostrar el capítulo actual al iniciar
        ActualizarTexto(ChapterManager.Instance?.capituloActual ?? 1);

        // Escuchar cambios
        ChapterManager.OnCapituloCambiado += ActualizarTexto;
    }

    void OnDestroy()
    {
        ChapterManager.OnCapituloCambiado -= ActualizarTexto;
    }

    void ActualizarTexto(int capitulo)
    {
        if (ChapterManager.Instance != null && textoCapitulo != null)
        {
            string nombre = ChapterManager.Instance.ObtenerNombreCapitulo(capitulo);
            textoCapitulo.text = $"Capítulo {capitulo}: {nombre}";
        }
    }
}
