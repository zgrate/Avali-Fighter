using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public Vector3 min;
    public Vector3 max;

    public float spawnerTimer = 2;
    private float lastSpawn = 0;

    public GameObject enemy;

    public bool paused = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetPaused(bool paused)
    {
        this.paused = paused;
    }
    // Update is called once per frame
    void Update()
    {
        if(!paused && Time.time > lastSpawn )
        {
            lastSpawn = Time.time + spawnerTimer;
            var enemyInst = Instantiate(enemy);
            enemyInst.transform.SetParent(gameObject.transform);
            enemyInst.transform.position = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
            enemyInst.SetActive(true);
        }
        
    }

    public void DestroyAll()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
