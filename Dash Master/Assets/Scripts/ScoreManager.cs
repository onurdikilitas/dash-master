using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score;
    public int highScore;
    public int diamond;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }

    void Start()
    {
        score = 0;
        PlayerPrefs.SetInt("score", score);
        diamond = PlayerPrefs.GetInt("diamond", 0);
    }

    void Update()
    {
        
    }

    public void incrementScore()
    {
        score += 1;
        UIManager.instance.ingameScore.text = "Score: " + score;
    }

    public void incrementDiamond()
    {
        diamond += 1;
    }
    
    public void stopScore()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("diamond", diamond);

        if (PlayerPrefs.HasKey("highScore"))
        {
            if(score > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }
}
