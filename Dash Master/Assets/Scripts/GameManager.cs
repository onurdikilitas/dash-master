using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameOver;

    public bool gamePaused;
    public GameObject ball;
    public GameObject platform;
    public Vector3 contPoint;

    public PlatformSpawner ps;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        gameOver = false;
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        UIManager.instance.GameStart();
        ps.StartSpawn();
    }

    public void GameOver()
    {
        //Stop camera movement when the game end
        Camera.main.transform.SetParent(null);

        ps.StopSpawn();

        //Set the gameOver values
        ScoreManager.instance.stopScore();
        UIManager.instance.GameOver();        
        gameOver = true;
    }

    public void PauseGame()
    {
        gamePaused = true;
        UIManager.instance.Paused();
        //ps.GetComponent<PlatformSpawner>().StopSpawn();
        ps.StopSpawn();
    }

    public void Continue()
    {
        //Safe platform spawn
        Vector3 platPoint = new Vector3(contPoint.x -= 1, contPoint.y, contPoint.z -= 1);
        Instantiate(platform, platPoint, Quaternion.identity);

        //Bring back the ball
        Vector3 ballPoint = new Vector3(contPoint.x - 1f, 0.911f, contPoint.z -1f);
        ball.transform.position = ballPoint;

        //Close pause window an restart platform spawn
        UIManager.instance.pausePanel.SetActive(false);
        ps.StartSpawn();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
