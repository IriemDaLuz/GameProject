using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    public static ChapterManager Instance;

    [Header("Capítulo actual")]
    public int capituloActual = 1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void CambiarCapitulo(int nuevoCapitulo)
    {
        capituloActual = nuevoCapitulo;

        string nombreCapitulo = ObtenerNombreCapitulo(nuevoCapitulo);
        ChapterTitleManager.Instance?.ShowChapter("Capítulo " + nuevoCapitulo, nombreCapitulo);

        MissionManager.Instance?.ActualizarMisionesPorCapitulo(nuevoCapitulo);

        Debug.Log($"Cambio al capítulo {nuevoCapitulo}: {nombreCapitulo}");
    }

    private string ObtenerNombreCapitulo(int capitulo)
    {
        switch (capitulo)
        {
            case 1: return "El Despertar";
            case 2: return "Sombras en los Pasillos";
            case 3: return "La Persecución Interna";
            case 4: return "El Juicio Final";
            default: return "Capítulo Desconocido";
        }
    }
}
