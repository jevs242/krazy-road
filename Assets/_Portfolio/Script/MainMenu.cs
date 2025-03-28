using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    [SerializeField]public int Gems;
    [SerializeField]private Material[] Color;
    [SerializeField]private int cost = 500;
    [SerializeField]private int[] bought; 
    [SerializeField]private GameObject Car;
    [SerializeField]private GameObject[] Hat;
    [SerializeField]private Renderer Body;
    [SerializeField]private Renderer Kart;
    [SerializeField]private Material KartMaterial;
    [SerializeField]private Text gemsText;
    [SerializeField]private AudioSource _clickplay;

    private bool _play = false;

    private void Start()
    {

        Time.timeScale = 1;
        Gems = PlayerPrefs.GetInt("Gem");
        UpdateGem();
        SetPlayer();
    }

    public void UpdateGem()
    {
        gemsText.text = Gems.ToString("#,##0");
    }

    public void Update()
    {
        if(_play)
        {
            Car.transform.position += new Vector3(2 * Time.deltaTime, 0, 0);
        }
    }

    public void PlayGame()
    {   
        _clickplay.Play();
        StartCoroutine(BreakSound(0.8f));
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator BreakSound(float time)
    {
        yield return new WaitForSeconds(time);
        _play = true;
        StartCoroutine(Play(2.0f));
    } 

    IEnumerator Play(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Level");
    }

    public void SetColorKart(int ColorN)
    {
        string Number = "Color" + ColorN;
        if (PlayerPrefs.GetInt(Number) == 0)
        {
            if (Gems >= cost)
            {
                Kart.material = Color[ColorN];
                Gems -= cost;
                PlayerPrefs.SetInt("Gem", Gems);
                UpdateGem();
                PlayerPrefs.SetInt(Number, 1);
                PlayerPrefs.SetInt("NowColorKart", ColorN);
            }
        }
        else
        {
            Kart.material = Color[ColorN];
            PlayerPrefs.SetInt("NowColorKart", ColorN);
        }
        SetPlayer();
    }

    public void SetColorPlayer(int ColorN)
    {
        string Number = "Color" + ColorN;
        if(PlayerPrefs.GetInt(Number) == 0)
        {
            if (Gems >= cost)
            {
                Body.material = Color[ColorN];
                Gems -= cost;
                PlayerPrefs.SetInt("Gem", Gems);
                UpdateGem();
                PlayerPrefs.SetInt(Number, 1);
                PlayerPrefs.SetInt("NowColorPlayer", ColorN);
            }
        }
        else if(PlayerPrefs.GetInt(Number) == 1)
        {
            Body.material = Color[ColorN];
            PlayerPrefs.SetInt("NowColorPlayer", ColorN);
        }
        SetPlayer();
    }

    
    public void SetHat(int nHat)
    {
        string Number = "Hat" + nHat;
        if (PlayerPrefs.GetInt(Number) == 0)
        {
            if (Gems >= cost)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Hat[i] != null)
                    {
                        Hat[i].SetActive(false);
                    }
                }
                if (Hat[nHat] != null)
                {
                    Hat[nHat].SetActive(false);
                }
                Gems -= cost;
                PlayerPrefs.SetInt("Gem", Gems);
                UpdateGem();
                PlayerPrefs.SetInt(Number, 1);
                PlayerPrefs.SetInt("NowHat", nHat);
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (Hat[i] != null)
                {
                    Hat[i].SetActive(false);
                }
            }
            if (Hat[nHat] != null)
            {
                Hat[nHat].SetActive(true);   
            }
            PlayerPrefs.SetInt("NowHat", nHat);
        }
        SetPlayer();
    }

    public void SetKartOriginal(int ColorN)
    {
        Kart.material = Color[ColorN];
        for (int i = 0; i < 4; i++)
        {
            if(Hat[i] != null)
            {
                Hat[i].SetActive(false);
            }
        }
        PlayerPrefs.SetInt("NowHat", 0);
        PlayerPrefs.SetInt("NowColorKart", ColorN);
    }

    public void SetPlayerOriginal(int ColorN)
    {
        Body.material = Color[ColorN];
        PlayerPrefs.SetInt("NowColorPlayer", ColorN);
    }

    private void SetPlayer()
    {

        if(PlayerPrefs.GetInt("Begin") == 0)
        {
            PlayerPrefs.SetInt("NowColorKart", 1);
            PlayerPrefs.SetInt("Begin", 1);
        }
        Kart.material = Color[PlayerPrefs.GetInt("NowColorKart")];
        Body.material = Color[PlayerPrefs.GetInt("NowColorPlayer")];

        if (Hat[PlayerPrefs.GetInt("NowHat")] != null)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Hat[i] != null)
                {
                    Hat[i].SetActive(false);
                }
            }
            Hat[PlayerPrefs.GetInt("NowHat")].SetActive(true);
        }
    }

    public void OpenLink()
    {
        Application.OpenURL("https://www.jevs.art/");
    }

    public void buygems()
    {
        Gems = PlayerPrefs.GetInt("Gem");
        UpdateGem();
    }

    public void Noads()
    {

    }

}
