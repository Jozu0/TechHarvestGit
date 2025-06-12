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
    public AudioClip moveDrone;
    public AudioClip hitRessource;
    public AudioClip destroyRessource;
    public AudioClip newItemAdded;
    public AudioClip colorChangeCube;
    public AudioClip enemyDying;
    public AudioClip enemyAttack;
    public AudioClip pushCube;
    public AudioClip enemyGetHit;
    public AudioClip enemyClap;
    public AudioClip randomScaryNoise1;
    public AudioClip randomScaryNoise2;
    public AudioClip doorOpen;
    public AudioClip pressurePlateActivate;
    public AudioClip caveNoise;
    public AudioClip click;
    
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
            if (sceneName == "MainMenuScene")
            {
                PlayMusic(sceneName,menuMusic);
                SoundFXManager.Instance.PauseSfxLoop();

            }else
            {
                PlayMusic(sceneName,backgroundNoise);
            }
        }
    }

    public void ToggleMusic()
    {
        MusicSource.mute = !MusicSource.mute ;
    }
    
    public void PlayMusic(string sceneName,AudioClip clip )
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

    public void PlayWalk(AudioClip clip)
    {
        WalkingSource.clip = clip;
        WalkingSource.loop = true;
        WalkingSource.Play();
    }

    public void StopWalking()
    {
        WalkingSource.Stop();
    }

    public void PlaySfxLoop(AudioClip clip)
    {
        SFXLoopSource.clip = clip;
        SFXLoopSource.loop = true;
        SFXLoopSource.Play();
    }
    
    public void PauseSfxLoop()
    {
        SFXLoopSource.Pause();
    }

    public void UnPauseSfxLoop()
    {
        SFXLoopSource.UnPause();
    }

    public void PlayLocalizedSFX(AudioClip clip)
    {
        
    }
}
