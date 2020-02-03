using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSpawnerScript : MonoBehaviour
{
    // Less means less time before a particle is spawned.
    public  float MinSpawnRate = 1f;
    public  float MaxSpawnrate = 2.5f;

    public const float MinY = 4f;
    public const float MaxY = 8f;

    public GameObject enemy;
    public float nextspawn;
    public List<GameObject> enemies;
    
    void Start()
    {
        nextspawn = 0.0f;
        enemies = new List<GameObject>();
    }

    void Update()
    {
        if (Time.time > nextspawn)
        {
            nextspawn = Random.Range(MinSpawnRate, MaxSpawnrate) + Time.time;
            var randY = Random.Range(MinY, MaxY) + 10f;
            var wheretospawn = new Vector2(transform.position.x, randY);
            enemies.Add(Instantiate(enemy, wheretospawn, Quaternion.identity));
        }     
    }
    
}
