using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleResult_Controller : MonoBehaviour
{
    [SerializeField] GameObject[] image_Character;
    [SerializeField] GameObject[] name_Character;
    [SerializeField] TextMeshProUGUI scoreResult;

    int scorePlayer1;
    int scorePlayer2;
    int indexPlayer1;
    int indexPlayer2;


    private void Start()
    {
        scorePlayer1 = PlayerPrefs.GetInt("ScorePlayer1");
        scorePlayer2 = PlayerPrefs.GetInt("ScorePlayer2");
        indexPlayer1 = PlayerPrefs.GetInt("IndexPlayer1");
        indexPlayer2 = PlayerPrefs.GetInt("IndexPlayer2");

        ResultDisplay();
    }

    void ResultDisplay()
    {
        if (scorePlayer1 < scorePlayer2)
        {
            image_Character[indexPlayer2].SetActive(true);
            name_Character[indexPlayer2].SetActive(true);
            scoreResult.text = "Score: " + scorePlayer2;
        }
        else if (scorePlayer1 > scorePlayer2)
        {
            image_Character[indexPlayer1].SetActive(true);
            name_Character[indexPlayer1].SetActive(true);
            scoreResult.text = "Score: " + scorePlayer1;
        }
    }

    public void Button_Replay()
    {
        SceneManager.LoadScene("BattleStart");
    }

    public void Button_MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
