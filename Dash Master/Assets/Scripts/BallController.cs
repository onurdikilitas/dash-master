using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public GameObject particle;
    [SerializeField] GameObject spawner;

    Rigidbody rb;
    bool startFlag = false;
    bool gameOverFlag = false;

    bool notDowned = true;
    bool paused = false;
    bool restart = false;
    Vector3 lastPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
    }

    void Update()
    {

        if (!gameOverFlag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!startFlag)
                {
                    startFlag = true;
                    rb.linearVelocity = new Vector3(speed, 0, 0);
                    GameManager.instance.StartGame();
                }
                else if (restart)
                {
                    rb.linearVelocity = new Vector3(speed, 0, 0);
                    restart = false;
                }
                else
                {
                    SwitchDirection();
                }
            }

            if (!Physics.Raycast(transform.position, Vector3.down, 1f))
            {
                if (notDowned)
                {
                    notDowned = false;
                    paused = true;
                    rb.isKinematic = true;
                    GameManager.instance.PauseGame();
                }
                else if (!paused)
                {
                    gameOverFlag = true;
                    GameManager.instance.GameOver();
                }

            }
        }
        /* Temizlenecek
        if (!startFlag) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.linearVelocity = new Vector3(speed, 0, 0);
                startFlag = true;
            }
        }

        if (!Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            gameOverFlag = true;
            rb.useGravity = true;
        }

        if (Input.GetMouseButtonDown(0) && !gameOverFlag) 
        {
            SwitchDirection();
        }
        */
    }

    public void Quit()
    {
        rb.isKinematic = false;
        GameManager.instance.GameOver();
    }

    public void Continue()
    {
        ScoreManager.instance.diamond -= 20;
        rb.isKinematic = false;
        paused = false;
        restart = true;
        GameManager.instance.Continue();
    }

    void SwitchDirection()
    {
        if(rb.linearVelocity.z > 0)
        {
            rb.linearVelocity = new Vector3(speed, 0, 0);
        }
        else if(rb.linearVelocity.x > 0)
        {
            rb.linearVelocity = new Vector3(0, 0, speed);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Diamond"))
        {
            Vector3 position = col.transform.position;
            Color cubeColor = col.gameObject.GetComponent<Renderer>().material.color;

            // Spawn particle
            SpawnParticleEffect(position, cubeColor);

            Destroy(col.gameObject);
            ScoreManager.instance.incrementDiamond();
        }
            
    }


    void SpawnParticleEffect(Vector3 position, Color color)
    {
        // Reference for particle instantiation
        GameObject particleInstance = Instantiate(particle, position, Quaternion.identity);

        // Match the color to diamond
        ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();
        var mainModule = particleSystem.main;
        mainModule.startColor = color;

        Destroy(particleInstance, 2f);
    }
}
