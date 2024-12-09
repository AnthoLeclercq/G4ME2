using UnityEngine;
using UnityEngine.Audio;

public class AudioControllerMenu : MonoBehaviour
{
    public AudioClip soundMenu;
    [Range(0f, 1f)]
    public float volumeMenu;
    [Range(0.1f, 2.5f)]
    public float speedSoundMenu;

    private AudioSource source; //Component that takes parameters from before

    void Awake()
    {
        gameObject.AddComponent<AudioSource>();
        source = GetComponent<AudioSource>();

        volumeMenu = 0.5f;
        speedSoundMenu = 1f;
    }

    void Start()
    {
        source.clip = soundMenu;
        source.volume = volumeMenu;
        source.pitch = speedSoundMenu;
    }

    void Update()
    {
        PlayAndPause();

        source.volume = volumeMenu;
        source.pitch = speedSoundMenu;
    }

    public void PlayAndPause()
    {
        if(!source.isPlaying && PlayerLife.instance.currentLive <= 0)
            source.Play();
        else
            source.Pause();
    }
}
