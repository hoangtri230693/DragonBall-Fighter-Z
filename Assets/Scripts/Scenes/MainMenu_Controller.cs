using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Controller : MonoBehaviour
{
    [SerializeField] GameObject[] menuItems;

    private float blinkSpeed = 0.5f;
    private int currentIndex = 0;

    private void Start()
    {
        UpdateMenuItems();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = (currentIndex - 1 + menuItems.Length) % menuItems.Length;
            UpdateMenuItems();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = (currentIndex + 1) % menuItems.Length;
            UpdateMenuItems();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectMenuItems();
        }
    }

    void UpdateMenuItems()
    {
        StopAllCoroutines();

        foreach(var item in menuItems)
        {
            item.SetActive(true);
        }

        StartCoroutine(Blink(menuItems[currentIndex]));
    }

    IEnumerator Blink(GameObject item)
    {
        while (true)
        {
            item.SetActive(true);
            yield return new WaitForSeconds(blinkSpeed);
            item.SetActive(false);
            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    void SelectMenuItems()
    {
        if (currentIndex == 0)
        {
            SceneManager.LoadScene("BattleSelection");
        }
        else if (currentIndex == 1)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (currentIndex == 2)
        {
            SceneManager.LoadScene("Options");
        }
        else if (currentIndex == 3)
        {
            Application.Quit();
        }
    }

    public void Button_PlayGame()
    {
        SceneManager.LoadScene("BattleSelection");
    }

    public void Button_Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Button_Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Button_Quit()
    {
        Application.Quit();
    }
}
