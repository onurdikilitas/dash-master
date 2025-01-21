using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;
    public GameObject[] diamonds;

    Vector3 lastPos;
    float size;

    void Start()
    {   
        lastPos = platform.transform.position;
        size = platform.transform.localScale.x;      
    }

    public void StartSpawn()
    {
        InvokeRepeating("Spawner", 0f, 0.2f);
    }

    public void StopSpawn()
    {
        CancelInvoke("Spawner");
    }

    void Update()
    {
        
        
    }

    void Spawner()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
            SpawnX();
        else if (rand == 1)
            SpawnZ();
    }

    //Platform in x way
    void SpawnX()
    {
        Vector3 pos = lastPos;
        pos.x += size;
        lastPos = pos;
        Instantiate(platform, pos, Quaternion.identity);

        Vector3 diaPos = lastPos;
        diaPos.y += 1;

        int rand = Random.Range(0, 10);
        if(rand < diamonds.Length)
            Instantiate(diamonds[rand], diaPos, diamonds[rand].transform.rotation);
    }

    //Platform in z way
    void SpawnZ()
    {
        Vector3 pos = lastPos;
        pos.z += size;
        lastPos = pos;
        Instantiate (platform, pos, Quaternion.identity);

        Vector3 diaPos = lastPos;
        diaPos.y += 1;

        int rand = Random.Range(0, 10);
        if (rand < diamonds.Length)
            Instantiate(diamonds[rand], diaPos, diamonds[rand].transform.rotation);
    }
}
