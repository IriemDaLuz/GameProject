using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [Header("Configuración de Parpadeo")]
    public Light lightSource;             
    public float minTime = 0.05f;        
    public float maxTime = 0.3f;          
    public bool flicker = true;          

    private void Start()
    {
        if (lightSource == null)
            lightSource = GetComponent<Light>();

        StartCoroutine(FlickerLoop());
    }

    private System.Collections.IEnumerator FlickerLoop()
    {
        while (flicker)
        {
            lightSource.enabled = !lightSource.enabled;
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void SetFlicker(bool state)
    {
        flicker = state;

        if (flicker)
            StartCoroutine(FlickerLoop());
        else
            lightSource.enabled = true;
    }
}
