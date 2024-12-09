using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
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

        volumeMenu = 1f;
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
        if(!source.isPlaying)
            source.Play();
    }

    public void ReplayGame()
    {
        source.Pause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
