using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_Controller : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Button_MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
