using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            ScoreManager.instance.incrementScore();
            GameManager.instance.contPoint = rb.transform.position;
            Invoke("Fall", 1f);
        }
            
    }

    private void Fall()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        Destroy(transform.parent.gameObject, 3f);
    }
}
