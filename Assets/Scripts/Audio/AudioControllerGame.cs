using UnityEngine;
using UnityEngine.Audio;

public class AudioControllerGame : MonoBehaviour
{
    public AudioClip soundGame;
    [Range(0f, 1f)]
    public float volumeGame;
    [Range(0.1f, 2.5f)]
    public float speedSoundGame;

    private AudioSource source; //Component that takes parameters from before

    void Awake()
    {
        gameObject.AddComponent<AudioSource>();
        source = GetComponent<AudioSource>();

        volumeGame = 0.2f;
        speedSoundGame = 1f;
    }

    void Start()
    {
        source.clip = soundGame;
        source.volume = volumeGame;
        source.pitch = speedSoundGame;
    }

    void Update()
    {
        PlayAndPause();

        source.volume = volumeGame;
        source.pitch = speedSoundGame;
    }

    //if the player is alive, the sound of the game is playing
    public void PlayAndPause()
    {
        if(!source.isPlaying && PlayerLife.instance.currentLive > 0)
            source.Play();

        if (PlayerLife.instance.currentLive <= 0)
            source.Pause();
    }
}
