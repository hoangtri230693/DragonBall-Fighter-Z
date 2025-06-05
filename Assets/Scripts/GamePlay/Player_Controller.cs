using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    [Header("Background MP")]
    [SerializeField] Image[] backgroundMP;

    [Header("MP Bar")]
    [SerializeField] Image[] fillMP;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI textScore;

    float maxMP = 500f;
    float currentMP;
    int currentBackground;
    public int currentScore = 1000;

    public bool canAttack = true;
    public bool canCharge = true;

    public GameObject character;

    private void Start()
    {
        if (gameObject.tag == "PlayerControl 1")
        {
            character = GameObject.FindGameObjectWithTag("Player 1");
        }
        if (gameObject.tag == "PlayerControl 2")
        {
            character = GameObject.FindGameObjectWithTag("Player 2");
        }

        currentMP = maxMP;
        currentBackground = backgroundMP.Length - 2;
        foreach (Image img in fillMP)
        {
            img.fillAmount = 1;
        }
        foreach (Image img in backgroundMP)
        {
            img.fillAmount = 0;
            img.gameObject.SetActive(false);
        }
        backgroundMP[currentBackground].gameObject.SetActive(true);

        textScore.text = "" + currentScore; 
    }

    private void Update()
    {
        if (character == null)
        {
            if (gameObject.tag == "PlayerControl 1")
            {
                character = GameObject.FindGameObjectWithTag("Player 1");
            }
            if (gameObject.tag == "PlayerControl 2")
            {
                character = GameObject.FindGameObjectWithTag("Player 2");
            }
        }
    }

    public void UseMP(float mp)
    {
        currentMP -= mp;
        HandleMP();
    }

    public void RestoreMP(float mp)
    {
        currentMP += mp;
        HandleMP();
    }

    void HandleMP()
    {
        // Xử lý tràn / hụt MP
        if (currentMP < 0)
        {
            if (currentBackground > 0)
            {
                currentBackground--;
                currentMP += maxMP;
            }
            else
            {
                currentMP = 0;
            }
        }
        else if (currentMP > maxMP)
        {
            if (currentBackground < backgroundMP.Length - 1)
            {
                currentBackground++;
                currentMP -= maxMP;
            }
            else
            {
                currentMP = maxMP;
            }
        }

        // Cập nhật trạng thái tấn công và tích năng lượng
        canAttack = currentBackground > 0 || currentMP > 0;
        canCharge = currentBackground < backgroundMP.Length - 1 || currentMP < maxMP;

        // Cập nhật hình ảnh thanh MP và background
        UpdateMPBackground();
        UpdateMPFill();
    }

    void UpdateMPBackground()
    {
        for (int i = 0; i < backgroundMP.Length; i++)
        {
            backgroundMP[i].gameObject.SetActive(i == currentBackground);
        }
    }

    void UpdateMPFill()
    {
        foreach (Image img in fillMP)
        {
            img.fillAmount = 0;
        }

        float remaining = currentMP % 100f;
        int fullBars = Mathf.FloorToInt(currentMP / 100f);
        fullBars = Mathf.Clamp(fullBars, 0, fillMP.Length);

        for (int i = 0; i < fullBars; i++)
        {
            fillMP[i].fillAmount = 1f;
        }

        if (fullBars < fillMP.Length)
        {
            fillMP[fullBars].fillAmount = remaining / 100f;
        }
    }

    public void UpdateScore(int score)
    {
        currentScore += score;
        textScore.text = "" + currentScore;
    }
}
