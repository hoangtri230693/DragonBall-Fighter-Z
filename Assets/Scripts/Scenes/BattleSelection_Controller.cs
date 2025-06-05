using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BattleSelection_Controller : MonoBehaviour
{
    [SerializeField] GameObject[] avatar_Character;
    [SerializeField] GameObject[] image_Character;
    [SerializeField] GameObject[] name_Character;
    [SerializeField] GameObject[] image_Arena;
    [SerializeField] GameObject[] name_Arena;
    [SerializeField] GameObject text_Player1;
    [SerializeField] GameObject text_Player2;
    [SerializeField] GameObject text_Arena;
    [SerializeField] GameObject button_NextArena;
    [SerializeField] GameObject button_PreviousArena;


    private float blinkSpeed = 0.5f;
    private int currentIndex = 0;
    private bool arenaSelect = false;


    private void Start()
    {
        text_Player1.SetActive(true);

        foreach (var item in avatar_Character)
        {
            item.SetActive(true);
        }

        UpdateMenuCharacter();
        UpdateImageCharacter();
        UpdateNameCharacter();
        EffectSounds_Controller.instance.PlayPlayer1Sound();
    }


    private void Update()
    {
        ControlUpdateInput();
    }

    void ControlUpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && arenaSelect == false)
        {
            currentIndex = (currentIndex - 9 + avatar_Character.Length) % avatar_Character.Length;
            UpdateMenuCharacter();
            UpdateImageCharacter();
            UpdateNameCharacter();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && arenaSelect == false)
        {
            currentIndex = (currentIndex + 9) % avatar_Character.Length;
            UpdateMenuCharacter();
            UpdateImageCharacter();
            UpdateNameCharacter();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && arenaSelect == false)
        {
            currentIndex = (currentIndex - 1 + avatar_Character.Length) % avatar_Character.Length;
            UpdateMenuCharacter();
            UpdateImageCharacter();
            UpdateNameCharacter();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && arenaSelect == false)
        {
            currentIndex = (currentIndex + 1) % avatar_Character.Length;
            UpdateMenuCharacter();
            UpdateImageCharacter();
            UpdateNameCharacter();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && arenaSelect == true)
        {
            currentIndex = (currentIndex - 1 + image_Arena.Length) % image_Arena.Length;
            UpdateImageArena();
            UpdateNameArena();
        }       
        else if (Input.GetKeyDown(KeyCode.RightArrow) && arenaSelect == true)
        {
            currentIndex = (currentIndex + 1) % image_Arena.Length;
            UpdateImageArena();
            UpdateNameArena();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectCharacterArena();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    void UpdateImageArena()
    {
        foreach (var item in image_Arena)
        {
            item.SetActive(false);
        }

        image_Arena[currentIndex].SetActive(true);
    }

    void UpdateNameArena()
    {
        foreach (var item in name_Arena)
        {
            item.SetActive(false);
        }

        name_Arena[currentIndex].SetActive(true);
    }

    void UpdateMenuCharacter()
    {
        StopAllCoroutines();

        foreach (var item in avatar_Character)
        {
            item.SetActive(true);
        }

        StartCoroutine(Blink(avatar_Character[currentIndex]));
    }

    void UpdateImageCharacter()
    {
        foreach (var item in image_Character)
        {
            item.SetActive(false);
        }

        image_Character[currentIndex].SetActive(true);
    }

    void UpdateNameCharacter()
    {
        foreach (var item in name_Character)
        {
            item.SetActive(false);
        }

        name_Character[currentIndex].SetActive(true);
    }

    IEnumerator Blink(GameObject character)
    {
        while (true)
        {
            character.SetActive(true);
            yield return new WaitForSeconds(blinkSpeed);
            character.SetActive(false);
            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    void SelectCharacterArena()
    {
        StopAllCoroutines();

        if (text_Player1.activeSelf)
        {
            text_Player1.SetActive(false);
            text_Player2.SetActive(true);
            PlayerPrefs.SetInt("Player1", currentIndex);
            PlayerPrefs.SetInt("IndexPlayer1", currentIndex);
            PlayerPrefs.Save();
            currentIndex = 0;
            UpdateMenuCharacter();
            UpdateImageCharacter();
            UpdateNameCharacter();
            EffectSounds_Controller.instance.PlayPlayer2Sound();
        }
        else if (text_Player2.activeSelf)
        {
            PlayerPrefs.SetInt("Player2", currentIndex);
            PlayerPrefs.SetInt("IndexPlayer2", currentIndex);
            PlayerPrefs.Save();

            text_Player2.SetActive(false);
            foreach (var item in avatar_Character)
            {
                item.SetActive(false);
            }
            foreach (var item in image_Character)
            {
                item.SetActive(false);
            }
            foreach (var item in name_Character)
            {
                item.SetActive(false);
            }

            currentIndex = 0;
            text_Arena.SetActive(true);
            image_Arena[currentIndex].SetActive(true);
            name_Arena[currentIndex].SetActive(true);
            button_NextArena.SetActive(true);
            button_PreviousArena.SetActive(true);
            arenaSelect = true;
            EffectSounds_Controller.instance.PlayArenaSound();
        }
        else if (text_Arena.activeSelf)
        {
            PlayerPrefs.SetInt("Arena", currentIndex);
            PlayerPrefs.Save();
            SceneManager.LoadScene("BattleStart");
        }
    }

    public void Button_NextStage()
    {
        currentIndex = (currentIndex + 1) % image_Arena.Length;
        UpdateImageArena();
        UpdateNameArena();
    }

    public void Button_PreviousStage()
    {
        currentIndex = (currentIndex - 1 + image_Arena.Length) % image_Arena.Length;
        UpdateImageArena();
        UpdateNameArena();
    }
}
