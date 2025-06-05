using UnityEngine;

public class EffectSounds_Controller : MonoBehaviour
{
    public static EffectSounds_Controller instance;

    [Header("Audio Soures")]
    [SerializeField] private AudioSource effectSound_Controller;

    [Header("Audio Clip")]
    [SerializeField] private AudioClip Ki_Final_Boom;
    [SerializeField] private AudioClip Player1;
    [SerializeField] private AudioClip Player2;
    [SerializeField] private AudioClip Arena;
    [SerializeField] private AudioClip Congratulations;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void PlaySoundEffect(AudioClip clip)
    {
        effectSound_Controller.PlayOneShot(clip);
    }

    public void StopSoundEffect()
    {
        effectSound_Controller.Stop();
    }
   
    public void PlayKiFinalBoomSound()
    {
        PlaySoundEffect(Ki_Final_Boom);
    }

    public void PlayPlayer1Sound()
    {
        PlaySoundEffect(Player1);
    }

    public void PlayPlayer2Sound()
    {
        PlaySoundEffect(Player2);
    }

    public void PlayArenaSound()
    {
        PlaySoundEffect(Arena);
    }

    public void PlayCongratulationSound()
    {
        PlaySoundEffect(Congratulations);
    }

    public void SetEffectMute(bool mute)
    {
        effectSound_Controller.mute = mute;
        PlayerPrefs.SetInt("EffectMute", mute ? 0 : 1);
        PlayerPrefs.Save();
    }

    public void SetEffectVolume(float volume)
    {
        effectSound_Controller.volume = volume;
        PlayerPrefs.SetFloat("EffectVolume", volume);
        PlayerPrefs.Save();
    }
}