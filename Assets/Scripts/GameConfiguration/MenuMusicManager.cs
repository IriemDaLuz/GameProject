using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    public static MenuMusicManager Instance;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Música")]
    public AudioClip musicaGeneral;  
    public AudioClip musicaJuego;    
    public AudioClip musicaCreditos;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
}
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ReproducirMusicaGeneral()
    {
        CambiarMusica(musicaGeneral);
    }

    public void ReproducirMusicaJuego()
    {
        CambiarMusica(musicaJuego);
    }

    public void ReproducirMusicaCreditos()
    {
        CambiarMusica(musicaCreditos);
    }

    private void CambiarMusica(AudioClip clip)
    {
        if (clip == null || audioSource == null) return;

        if (audioSource.clip == clip && audioSource.isPlaying)
            return; 

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
