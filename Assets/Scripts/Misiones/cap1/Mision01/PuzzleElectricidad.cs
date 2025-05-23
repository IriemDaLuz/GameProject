using UnityEngine;
using System.Collections;
using System.Linq;

public class PuzzleElectricidad : MonoBehaviour
{
    public InterruptorInteractivo[] interruptores;
    public Light[] lucesAEncender;
    public AudioSource audioFuente;
    public AudioClip sonidoElectricidad;
    public float tiempoParpadeo = 0.15f;
    public int cantidadParpadeos = 3;
    public GameObject panelAyuda;

    private bool resuelto = false;

    private void Start()
    {
         lucesAEncender = GameObject.FindGameObjectsWithTag("LuzPuzzle")
             .Select(go => go.GetComponent<Light>())
             .Where(l => l != null)
             .ToArray();
    }

    public void VerificarCombinacion()
    {
        if (resuelto) return;

        var solucion = new InterruptorInteractivo.Estado[]
        {
            InterruptorInteractivo.Estado.ON,
            InterruptorInteractivo.Estado.OFF,
            InterruptorInteractivo.Estado.ON,
            InterruptorInteractivo.Estado.BLOQUEADO,
            InterruptorInteractivo.Estado.ON,
            InterruptorInteractivo.Estado.OFF,
            InterruptorInteractivo.Estado.ON,
            InterruptorInteractivo.Estado.OFF,
            InterruptorInteractivo.Estado.OFF
        };

        for (int i = 0; i < interruptores.Length; i++)
        {
            if (interruptores[i].estadoActual != solucion[i])
            {
                Debug.Log("Combinación incorrecta");
                return;
            }
        }

        Debug.Log("Electricidad restaurada");
        resuelto = true;
        StartCoroutine(ActivarLucesConEfecto());
        MissionManager.Instance.CompletarMisionElectricidad();

        if (panelAyuda != null) panelAyuda.SetActive(false);
    }

    private IEnumerator ActivarLucesConEfecto()
    {
        float duracionTotal = (tiempoParpadeo * 2f) * cantidadParpadeos;

        if (audioFuente != null && sonidoElectricidad != null)
        {
            audioFuente.PlayOneShot(sonidoElectricidad);
        }

        for (int i = 0; i < cantidadParpadeos; i++)
        {
            foreach (Light luz in lucesAEncender)
            {
                if (luz != null) luz.enabled = false;
            }
            yield return new WaitForSeconds(tiempoParpadeo);

            foreach (Light luz in lucesAEncender)
            {
                if (luz != null) luz.enabled = true;
            }
            yield return new WaitForSeconds(tiempoParpadeo);
        }

        if (audioFuente != null && audioFuente.isPlaying)
        {
            audioFuente.Stop();
        }
    }
}
