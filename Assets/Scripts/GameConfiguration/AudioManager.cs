using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1.0f);
        audioSource.volume = savedVolume;

        bool isMuted = PlayerPrefs.GetInt("Mute", 0) == 1;
        audioSource.mute = isMuted;
    }
}
