using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options_Controller : MonoBehaviour
{
    [SerializeField] GameObject[] menuOptions;
    [SerializeField] GameObject popupSoundSettings; 
    [SerializeField] Slider change_MusicVolume;
    [SerializeField] Slider change_EffectVolume;
    [SerializeField] Toggle shutdown_MusicVolume;
    [SerializeField] Toggle shutdown_EffectVolume;

    private float blinkSpeed = 0.5f;
    private int currentIndex = 0;

    private void Start()
    {
        UpdateMenuOptions();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = (currentIndex - 1 + menuOptions.Length) % menuOptions.Length;
            UpdateMenuOptions();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = (currentIndex + 1) % menuOptions.Length;
            UpdateMenuOptions();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectMenuOptions();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void UpdateMenuOptions()
    {
        StopAllCoroutines();

        foreach (var item in menuOptions)
        {
            item.SetActive(true);
        }

        StartCoroutine(Blink(menuOptions[currentIndex]));
    }

    IEnumerator Blink(GameObject option)
    {
        while (true)
        {
            menuOptions[currentIndex].SetActive(true);
            yield return new WaitForSeconds(blinkSpeed);
            menuOptions[currentIndex].SetActive(false);
            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    void SelectMenuOptions()
    {
        if (currentIndex == 0)
        {
            Button_SoundSettings();
        }
        else if (currentIndex == 1)
        {
            Button_MainMenu();
        }
    }

    public void Button_SoundSettings()
    {
        StopAllCoroutines();

        foreach (var item in menuOptions)
        {
            item.SetActive(false);
        }

        // Lấy dữ liệu đã lưu, cập nhật vào UI
        change_MusicVolume.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        change_EffectVolume.value = PlayerPrefs.GetFloat("EffectVolume", 1);
        shutdown_MusicVolume.isOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        shutdown_EffectVolume.isOn = PlayerPrefs.GetInt("EffectOn", 1) == 1;

        popupSoundSettings.SetActive(true);
    }

    public void Button_MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Button_SaveSoundSettings()
    {
        // Lưu âm lượng
        PlayerPrefs.SetFloat("MusicVolume", change_MusicVolume.value);
        PlayerPrefs.SetFloat("EffectVolume", change_EffectVolume.value);

        // Lưu trạng thái bật/tắt nhạc
        PlayerPrefs.SetInt("MusicOn", shutdown_MusicVolume.isOn ? 1 : 0);
        PlayerPrefs.SetInt("EffectOn", shutdown_EffectVolume.isOn ? 1 : 0);

        // Cập nhật hệ thống âm thanh trong game
        MusicSounds_Controller.instance.SetMusicVolume(change_MusicVolume.value);
        MusicSounds_Controller.instance.SetMusicMute(!shutdown_MusicVolume.isOn);

        EffectSounds_Controller.instance.SetEffectVolume(change_EffectVolume.value);
        EffectSounds_Controller.instance.SetEffectMute(!shutdown_EffectVolume.isOn);

        // Lưu lại vào bộ nhớ
        PlayerPrefs.Save();
    }

    public void Button_BackSoundSettings()
    {
        popupSoundSettings.SetActive(false);

        foreach (var item in menuOptions)
        {
            item.SetActive(true);
        }

        StartCoroutine(Blink(menuOptions[currentIndex]));
    }
}
