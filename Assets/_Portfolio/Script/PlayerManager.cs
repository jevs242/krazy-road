using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool isGameStarted;
    public static float Distance;
    public static int Coins;
    public static int Mph;


    public int highscore;
    private int iTotal;
    private int GemsTotal = 0;

    [SerializeField]private GameObject gameOverPanel;
    [SerializeField]private GameObject gamePanel;
    [SerializeField]private GameObject startingText;
    [SerializeField]private GameObject ImagePanel;
    [SerializeField]private Text coinsText;
    [SerializeField]private Text distanceText;
    [SerializeField]private Text TotalText;
    [SerializeField]private Text MphText;
    [SerializeField]private Text highscoreText;

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        Distance = 0;
        Coins = 0;
        gamePanel.SetActive(false);
        GemsTotal = PlayerPrefs.GetInt("Gem");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameOver)
        {
            if(PlayerPrefs.GetInt("highscore") < iTotal)
            {
                PlayerPrefs.SetInt("highscore", iTotal);
            }
            highscoreText.text = "HighScore\n" + PlayerPrefs.GetInt("highscore").ToString("#,##0");
            PlayerPrefs.SetInt("Gem", GemsTotal + Coins );
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            gamePanel.SetActive(false);
        }
        coinsText.text = "" + Coins.ToString("#,##0");
        
        distanceText.text = "" + Distance.ToString("#,##0");
        if(Coins == 0 )
        {
            iTotal = (int)Distance;
        }
        else
        {
            iTotal = Coins * (int)Distance;
        }
        TotalText.text = " S c o r e \n" + iTotal.ToString("#,##0");
        MphText.text = Mph.ToString("");
        

        if (SwipeManager.tap)
        {
           if(!isGameStarted)
           {
                gamePanel.SetActive(true);
           }
            isGameStarted = true;
            Destroy(startingText);
            Destroy(ImagePanel);
        }
    }

}
