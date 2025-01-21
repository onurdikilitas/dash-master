using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameObject startText;

    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI highScoreMainMenu;
    public TextMeshProUGUI diamond;
    public TextMeshProUGUI ingameScore;

    public GameObject pausePanel;
    public TextMeshProUGUI diamondP;
    public Button continueButton;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        highScoreMainMenu.text = "High Score: " + PlayerPrefs.GetInt("highScore").ToString();
    }

    public void GameStart()
    {
        startText.GetComponent<Animator>().Play("textDown");
        startPanel.GetComponent<Animator>().Play("textUp");
        ingameScore.gameObject.SetActive(true);
    }
    
    public void GameOver()
    {
        pausePanel.SetActive(false);
        score.text = PlayerPrefs.GetInt("score").ToString();
        highScore.text = PlayerPrefs.GetInt("highScore").ToString();
        diamond.text = PlayerPrefs.GetInt("diamond").ToString();
        gameOverPanel.SetActive(true);
        ingameScore.gameObject.SetActive(false);
    }

    public void Paused()
    {
        pausePanel.SetActive(true);
        if(ScoreManager.instance.diamond < 20)
        {
            pausePanel.GetComponent<Animator>().Play("pausePanelZero");
            continueButton.interactable = false;
        }
        else
        {
            pausePanel.GetComponent<Animator>().Play("pausePanel");
            continueButton.interactable = true;
        }
        diamondP.text = ScoreManager.instance.diamond.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        
    }
}
