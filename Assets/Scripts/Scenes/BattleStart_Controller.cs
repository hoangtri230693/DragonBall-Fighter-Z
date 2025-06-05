using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleStart_Controller : MonoBehaviour
{
    [Header("Battle Start")]
    [SerializeField] GameObject[] menu_Background_Arena;
    [SerializeField] GameObject[] menu_Character_Prefab;
    [SerializeField] GameObject[] player1_Avatar_Character;
    [SerializeField] GameObject[] player2_Avatar_Character;
    [SerializeField] Player_Controller playerCtrl1;
    [SerializeField] Player_Controller playerCtrl2;
    private GameObject player1;
    private GameObject player2;

    [Header("Battle Timer")]
    private float currentTime = 60f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI countdownText;
    public bool isRunning = false;


    private void Start()
    {
        GetArena();
        GetCharacter();
        GetAvatar();
        StartTimer();
    }

    private void Update()
    {
        if (isRunning)
        {
            UpdateTimer();
            UpdateAvatar();
        }    
    }

    void GetArena()
    {
        int arenaIndex = PlayerPrefs.GetInt("Arena");
        foreach (var item in menu_Background_Arena)
        {
            item.SetActive(false);
        }
        menu_Background_Arena[arenaIndex].SetActive(true);
    }

    void GetCharacter()
    {
        player1 = Instantiate (menu_Character_Prefab[PlayerPrefs.GetInt("Player1")], new Vector3(-6f, -2.5f, 0), Quaternion.identity);
        player1.tag = "Player 1";

        player2 = Instantiate(menu_Character_Prefab[PlayerPrefs.GetInt("Player2")], new Vector3(6f, -2.5f, 0), Quaternion.identity);
        player2.tag = "Player 2";
    }

    void GetAvatar()
    {
        int avatarIndex1 = PlayerPrefs.GetInt("Player1");
        player1_Avatar_Character[avatarIndex1].SetActive(true);

        int avatarIndex2 = PlayerPrefs.GetInt("Player2");
        player2_Avatar_Character[avatarIndex2].SetActive(true);      
    }

    void StartTimer()
    {
        timerText.text = "";
        countdownText.text = "";
        StartCoroutine(CountdownBeforeStart());
    }

    void EndTimer()
    {
        currentTime = 0;
        isRunning = false; // Hết giờ
        SaveResult();
        SceneManager.LoadScene("BattleResult");
    }

    IEnumerator CountdownBeforeStart()
    {
        string[] countdowns = { "3", "2", "1", "FIGHT!" };
        foreach (string count in countdowns)
        {
            countdownText.text = count;
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "";
        isRunning = true;
        // Bắt đầu
    }

    void SaveResult()
    {
        PlayerPrefs.SetInt("ScorePlayer1", playerCtrl1.currentScore);
        PlayerPrefs.SetInt("ScorePlayer2", playerCtrl2.currentScore);
    }

    int GetPrefabIndexByName(string characterName)
    {
        for (int i = 0; i < menu_Character_Prefab.Length; i++)
        {
            if (menu_Character_Prefab[i].name == characterName)
            {
                return i;
            }
        }
        return -1;
    }

    void UpdateAvatar()
    {
        if (playerCtrl1.character != player1)
        {
            player1 = playerCtrl1.character;
            int player1Index = GetPrefabIndexByName(player1.name.Replace("(Clone)", "").Trim());
            Debug.Log("Name Player 1: " + GetPrefabIndexByName(player1.name.Replace("(Clone)", "").Trim()));
            foreach (var item in player1_Avatar_Character)
            {
                item.SetActive(false);
            }
            player1_Avatar_Character[player1Index].SetActive(true);
            PlayerPrefs.SetInt("IndexPlayer1", player1Index);
            Debug.Log("IndexPlayer1: " + player1Index);      
        }
        if (playerCtrl2.character != player2)
        {
            player2 = playerCtrl2.character;
            int player2Index = GetPrefabIndexByName(player2.name.Replace("(Clone)", "").Trim());
            Debug.Log("Name Player 2: " + GetPrefabIndexByName(player2.name.Replace("(Clone)", "").Trim()));
            foreach (var item in player2_Avatar_Character)
            {
                item.SetActive(false);
            }
            player2_Avatar_Character[player2Index].SetActive(true);
            PlayerPrefs.SetInt("IndexPlayer2", player2Index);
            Debug.Log("IndexPlayer2: " + player2Index);
        }
    }

    void UpdateTimer()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                EndTimer();
            }
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
