using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton Variable
    public static AudioManager Instance = null;

    // Scriptable Object
    [SerializeField] private Audios audios;

    // Audio Source
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        // Singleton Declaration
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBounceAudio()
    {
        audioSource.PlayOneShot(audios.BounceSFX);
    }

    public void PlayScoreAudio()
    {
        audioSource.PlayOneShot(audios.ScoredSFX);
    }
}
