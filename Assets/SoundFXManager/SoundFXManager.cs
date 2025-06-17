using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance { get; private set; }
    
    [Header("---------------Audio Sources---------------")]
    [SerializeField] public AudioSource MusicSource;
    [SerializeField] public AudioSource SFXSource;
    [SerializeField] public AudioMixer AudioMixer;

    [Header("---------------Audio Clip---------------")]
    public AudioClip menuMusic;
    public AudioClip cityBuilderMusic;
    public AudioClip shootingMusic;
    public AudioClip hitRessourceFX;
    public AudioClip destroyRessourceFX;
    public AudioClip newItemAddedFX;
    public AudioClip skillButtonFX;
    public AudioClip craftingButtonFX;
    public AudioClip buildingChangeFX;
    public AudioClip upgradeBuildingFX;
    public AudioClip buttonClickFX;
    
    private string currentScene = "";


    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre les scènes
        }
        else
        {
            Destroy(gameObject); // Évite les doublons si une autre instance existe
        }
    }
    
    private void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName != currentScene)
        {
            currentScene = sceneName;
            switch (sceneName)
            {
                case "MainMenuScene":
                {
                    PlayMusic(menuMusic);
                    break;
                }
                case "LevelFieldScene":
                {
                    PlayMusic(shootingMusic);
                    break;
                }
                case "CityBuilderScene":
                {
                    PlayMusic(cityBuilderMusic);
                    break;
                }
            }
        }
    }

    public void ToggleMusic()
    {
        MusicSource.mute = !MusicSource.mute ;
    }
    
    public void PlayMusic(AudioClip clip )
    {
        if (MusicSource != null)
        {
                MusicSource.clip = clip;
                MusicSource.loop = true; // Set the music to loop
                MusicSource.Play();
        }               
        else
        {
            Debug.LogWarning("MusicSource ou Backgroundmusic n'est pas assigné !");
        }
    }

    public void PlaySfx(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

  
}
