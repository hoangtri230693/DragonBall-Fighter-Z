using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame_Controller : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text_PressAnyButton;
    private float blinkSpeed = 0.5f;

    private void Start()
    {
        UpdateSoundSettings();
        StartCoroutine(Blink());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            text_PressAnyButton.alpha = 1f;
            yield return new WaitForSeconds(blinkSpeed);
            text_PressAnyButton.alpha = 0f;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
    void UpdateSoundSettings()
    {
        float change_MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        float change_EffectVolume = PlayerPrefs.GetFloat("EffectVolume", 1);
        bool shutdown_MusicVolume = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        bool shutdown_EffectVolume = PlayerPrefs.GetInt("EffectOn", 1) == 1;

        MusicSounds_Controller.instance.SetMusicVolume(change_MusicVolume);
        MusicSounds_Controller.instance.SetMusicMute(!shutdown_MusicVolume);
        EffectSounds_Controller.instance.SetEffectVolume(change_EffectVolume);
        EffectSounds_Controller.instance.SetEffectMute(!shutdown_EffectVolume);
    }
}
