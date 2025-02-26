using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public Animator anim;
    public GameObject caculatorPage;
    public Snack snack;
    public GameObject startMenu;

    public int score = 0;
    int highScore;

    void Start()
    {
        scoreText.text = score.ToString();
        anim.enabled = false;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HindCaculatorPage();
        }
    }

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();

        scoreText.color = Color.green;
        scoreText.color = Color.white;

        anim.enabled = true;
        anim.Play("scoreAnimation");

    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void ShowCaculatorPage(int dieWay)
    {
        caculatorPage.SetActive(true);
        snack.stopSnack();

        TMP_Text scoreText = caculatorPage.transform.Find("score").GetComponent<TMP_Text>();
        TMP_Text infoText = caculatorPage.transform.Find("info").GetComponent<TMP_Text>();
        TMP_Text recordText = caculatorPage.transform.Find("record").GetComponent<TMP_Text>();
        scoreText.text = "你的分數: " + score.ToString();


        GameObject Congratulations = caculatorPage.transform.Find("Congratulations").gameObject;
        Congratulations.SetActive(false);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
            Congratulations.SetActive(true);
            highScore = score;
        }
        recordText.text = "最高分: " + highScore;

        switch (dieWay)
        {
            case 0:
                infoText.text = "死因: " + "你撞到了牆壁";
                break;
            case 1:
                infoText.text = "死因: " + "你撞到了自己";
                break;
        }
    }

    void HindCaculatorPage()
    {
        caculatorPage.SetActive(false);
        snack.ResetScence();
    }

    public void CaculatorPageX_ButtonOnClick()
    {
        HindCaculatorPage();
    }

    public void BackToMenu_ButtonOnClick()
    {
        HindCaculatorPage();
        snack.ResetScence();
        startMenu.SetActive(true);
    }
}
