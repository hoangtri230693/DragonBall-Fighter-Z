using UnityEngine;

public class CharacterSounds_Controller : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource characterSound_Controller;

    [Header("Audio Clip")]
    [SerializeField] private AudioClip Fly;
    [SerializeField] private AudioClip Move;
    [SerializeField] private AudioClip Attack;
    [SerializeField] private AudioClip Up_Energy;
    [SerializeField] private AudioClip Ki_Base;
    [SerializeField] private AudioClip Ki_Final;
    [SerializeField] private AudioClip Up_Level;
    [SerializeField] private AudioClip FusionDance;
    [SerializeField] private AudioClip Ki_Kamehameha;
    [SerializeField] private AudioClip Ki_Kamehameha_Bigbang;
    [SerializeField] private AudioClip Ki_Kamehameha_Final;
    [SerializeField] private AudioClip Ki_DragonFist;

    private void Start()
    {
        float change_CharVolume = PlayerPrefs.GetFloat("EffectVolume", 1);
        bool shutdown_CharVolume = PlayerPrefs.GetInt("EffectOn", 1) == 1;
        characterSound_Controller.volume = change_CharVolume;
        characterSound_Controller.mute = !shutdown_CharVolume;
    }

    private void PlaySoundCharacter(AudioClip clip)
    {
        characterSound_Controller.PlayOneShot(clip);
    }

    public void StopSoundCharacter()
    {
        characterSound_Controller.Stop();
    }

    public void PlayFlySound()
    {
        PlaySoundCharacter(Fly);
    }

    public void PlayMoveSound()
    {
        PlaySoundCharacter(Move);
    }

    public void PlayAttackSound()
    {
        characterSound_Controller.loop = true;
        characterSound_Controller.clip = Attack;
        characterSound_Controller.Play();
    }

    public void PlayUpEnergySound()
    {
        characterSound_Controller.loop = true;
        characterSound_Controller.clip = Up_Energy;
        characterSound_Controller.Play();
    }

    public void PlayKiBaseSound()
    {
        PlaySoundCharacter(Ki_Base);
    }

    public void PlayKiFinalSound()
    {
        PlaySoundCharacter(Ki_Final);
    }

    public void PlayUpLevelSound()
    {
        PlaySoundCharacter(Up_Level);
    }

    public void PlayFusionDanceSound()
    {
        PlaySoundCharacter(FusionDance);
    }

    public void PlayKiKamehamehaSound()
    {
        PlaySoundCharacter(Ki_Kamehameha);
    }

    public void PlayKiKamehamehaBigbangSound()
    {
        PlaySoundCharacter(Ki_Kamehameha_Bigbang);
    }

    public void PlayKiKamehamehaFinalSound()
    {
        PlaySoundCharacter(Ki_Kamehameha_Final);
    }

    public void PlayKiDragonFistSound()
    {
        PlaySoundCharacter(Ki_DragonFist);
    }

    public void SetCharacterMute(bool mute)
    {
        characterSound_Controller.mute = mute;
        PlayerPrefs.SetInt("CharacterMute", mute ? 0 : 1);
        PlayerPrefs.Save();
    }

    public void SetCharacterVolume(float volume)
    {
        characterSound_Controller.volume = volume;
        PlayerPrefs.SetFloat("CharacterVolume", volume);
        PlayerPrefs.Save();
    }
}
