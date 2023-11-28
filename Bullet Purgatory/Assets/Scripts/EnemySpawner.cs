using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Variables

    //How many enemies to spawn
    public int spawnNumber;

    //Type of enemies to spawn
    public Vector3 spawnPosition;
    public GameObject enemyPrefab;

    //How fast enemies spawn
    public float spawnRate;

    //Current wave
    private int waves = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= waves; i++)
        {


        }
    }

    //Functions

    //IEnumerators
    private IEnumerator SpawnEnemy(float spawnRate, GameObject Enemy, Vector3 spawnPosition)
    {
        GameObject EnemySpawn = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemySpawn.transform.position = spawnPosition;
        //Each Enemy will have a different movement and shoot type
     
        yield return new WaitForSeconds(spawnRate);
    }
}
