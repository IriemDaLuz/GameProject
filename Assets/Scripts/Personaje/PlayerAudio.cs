using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerAudio : MonoBehaviour
{
    [Header("Pasos")]
    public AudioClip[] sonidosPasos;
    public float tiempoEntrePasos = 0.6f;
    private float contadorPasos;
    private AudioSource pasosSource;

    [Header("Respiración")]
    public AudioSource respiracionSource;
    public AudioClip respiracionNormal;

    [Header("Latidos")]
    public AudioSource latidoSource;
    public AudioClip latidoNormal;

    private CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();

        pasosSource = gameObject.AddComponent<AudioSource>();
        pasosSource.spatialBlend = 1f;
        pasosSource.playOnAwake = false;

        respiracionSource.clip = respiracionNormal;
        respiracionSource.loop = true;
        respiracionSource.Play();

        latidoSource.clip = latidoNormal;
        latidoSource.loop = true;
        latidoSource.Play();
    }

    void Update()
    {
        ReproducirPasos();
    }

    void ReproducirPasos()
    {
        if (cc.isGrounded && cc.velocity.magnitude > 0.1f)
        {
            contadorPasos -= Time.deltaTime;
            if (contadorPasos <= 0f && sonidosPasos.Length > 0)
            {
                AudioClip clip = sonidosPasos[Random.Range(0, sonidosPasos.Length)];
                pasosSource.PlayOneShot(clip);
                contadorPasos = tiempoEntrePasos;
            }
        }
        else
        {
            contadorPasos = 0f;
        }
    }

    // Opcional: cambiar sonidos desde otros scripts (como HUD o salud)
    public void CambiarLatido(AudioClip nuevoLatido, float volumen = 1f, float pitch = 1f)
    {
        latidoSource.clip = nuevoLatido;
        latidoSource.volume = volumen;
        latidoSource.pitch = pitch;
        latidoSource.Play();
    }

    public void CambiarRespiracion(AudioClip nuevoClip, float volumen = 1f)
    {
        respiracionSource.clip = nuevoClip;
        respiracionSource.volume = volumen;
        respiracionSource.Play();
    }
}
