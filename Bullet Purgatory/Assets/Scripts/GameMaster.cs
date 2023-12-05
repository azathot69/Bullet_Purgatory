using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    //Game Objects
    [SerializeField] private GameObject spawner1;
    [SerializeField] private GameObject spawner2;
    [SerializeField] private GameObject spawner3;
    public GameObject boss;

    //Variables
    public int enemiesDefeated = 0;
    public int currentWave = 0;
    public int totalWave = 1;

    // Start is called before the first frame update
    void Start()
    {
        enemiesDefeated = 0;
        spawner1.GetComponent<EnemySpawner>().Switch(true);
        //spawner2.GetComponent<EnemySpawner>().Switch(false);
        //spawner3.GetComponent<EnemySpawner>().Switch(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesDefeated >= 3)
        {
            spawner1.GetComponent<EnemySpawner>().Switch(false); ;

        }
    }
}
