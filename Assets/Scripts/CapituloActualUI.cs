using UnityEngine;
using TMPro;

public class CapituloActualUI : MonoBehaviour
{
    public TMP_Text textoCapitulo;

    private void Start()
    {
        ActualizarTexto(ChapterManager.Instance.capituloActual);
    }

    private void OnEnable()
    {
        ChapterManager.OnCapituloCambiado += ActualizarTexto;
    }

    private void OnDisable()
    {
        ChapterManager.OnCapituloCambiado -= ActualizarTexto;
    }

    private void ActualizarTexto(int numeroCapitulo)
    {
        if (textoCapitulo == null) return;

        string nombre = ChapterManager.Instance.ObtenerNombreCapitulo(numeroCapitulo);
        textoCapitulo.text = $"Capítulo {numeroCapitulo} – {nombre}";
    }
}
