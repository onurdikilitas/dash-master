using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform ball;
    private Vector3 offset;
    [SerializeField] private float lerpRate = 5f;
    private bool gameOver = false;

    public bool GameOver
    {
        set { gameOver = value; }
    }

    void Start()
    {
        //Initial distance between ball and camera
        offset = ball.position - transform.position;
        
    }

    void LateUpdate()
    {
        if (!gameOver)
        {
            Follow();
        }
    }

    void Follow()
    {
        Vector3 targetPos = ball.position - offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpRate * Time.deltaTime);
    }
}
