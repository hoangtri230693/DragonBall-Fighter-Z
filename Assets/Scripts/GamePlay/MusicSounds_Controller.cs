using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSounds_Controller : MonoBehaviour
{
    public static MusicSounds_Controller instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSound_Controller;

    [Header("Audio Clip")]
    [SerializeField] private AudioClip MusicSound_StartGame;
    [SerializeField] private AudioClip MusicSound_MainMenu;
    [SerializeField] private AudioClip MusicSound_BattleSelection;
    [SerializeField] private AudioClip MusicSound_Tutorial;
    [SerializeField] private AudioClip MusicSound_Options;
    [SerializeField] private AudioClip MusicSound_BattleResult;
    [SerializeField] private AudioClip MusicSound_123Fight;
    
 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusicByScene(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void PlayMusicSound(AudioClip clip)
    {
        musicSound_Controller.Stop();
        musicSound_Controller.clip = clip;
        musicSound_Controller.loop = true;
        musicSound_Controller.volume = 0.8f;
        musicSound_Controller.Play();
    }

    private void PlayOneShotSound(AudioClip clip)
    {
        musicSound_Controller.Stop();
        musicSound_Controller.PlayOneShot(clip);
    }

    public void StopMusicSound()
    {
        musicSound_Controller.Stop();
    }

    private void PlayMusicByScene(string sceneName)
    {
        if (sceneName == "StartGame") PlayMusicSound(MusicSound_StartGame);
        else if (sceneName == "MainMenu") PlayMusicSound(MusicSound_MainMenu);
        else if (sceneName == "Tutorial") PlayMusicSound(MusicSound_Tutorial);
        else if (sceneName == "Options") PlayMusicSound(MusicSound_Options);
        else if (sceneName == "BattleSelection") PlayMusicSound(MusicSound_BattleSelection);
        else if (sceneName == "BattleStart") PlayOneShotSound(MusicSound_123Fight);
        else if (sceneName == "BattleResult") PlayOneShotSound(MusicSound_BattleResult);
        else StopMusicSound();  
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicByScene(scene.name);
    }

    public void SetMusicMute(bool mute)
    {
        musicSound_Controller.mute = mute;
        PlayerPrefs.SetInt("MusicMute", mute ? 0 : 1);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        musicSound_Controller.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
}
