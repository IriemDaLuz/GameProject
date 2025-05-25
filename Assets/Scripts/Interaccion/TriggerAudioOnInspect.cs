using UnityEngine;

public class TriggerAudioOnInspect : MonoBehaviour
{
    public AudioClip audioClip;
    private bool yaReproducido = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!yaReproducido && other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            yaReproducido = true;
        }
    }
}
