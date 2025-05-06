using UnityEngine;
using System.Collections.Generic;

public class ControladorLucesIniciales : MonoBehaviour
{
    public List<Light> lucesNormales = new List<Light>();
    public Light luzInicial;
    public bool energiaActiva = false;

    void Start()
    {
        if (luzInicial != null)
            luzInicial.enabled = true;

        foreach (Light luz in lucesNormales)
        {
            if (luz != null)
                luz.enabled = false;
        }
    }

    public void ActivarEnergia()
    {
        energiaActiva = true;

        foreach (Light luz in lucesNormales)
        {
            if (luz != null)
                luz.enabled = true;
        }
    }
}
