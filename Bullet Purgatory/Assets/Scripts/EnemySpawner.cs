using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

/*
[Acuna, Joseph] [Hernandez, Max]
[12/06/23]
Spawns enemies and controls other spawners + boss spawn
*/

public class EnemySpawner : MonoBehaviour
{
    //Variables
    public float minX;
    public float maxX;
    private bool movingRight = true;
    public float speed = 10f;

    public float startX;

    public int level1Spawn;
    public int level2Spawn;
    public int level3Spawn;

    //How fast enemies spawn
    public float spawnRate;
    public bool canSpawn = true;
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;

    [SerializeField] private int enemiesSpawned;

    //Other Spawners
    [SerializeField] private GameObject sideSpawner1; // Spawns from the Left
    [SerializeField] private GameObject sideSpawner2; //Spawns from the right
    public GameObject playerShip;
    private GameObject tempEnemy;

    //Destroy BG
    public GameObject plane1;
    public GameObject plane2;
    public GameObject plane3;

    public GameObject UserInter;
    private bool nextLevel = true;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        StartCoroutine(Spawner());

        sideSpawner1.SetActive(false);
        sideSpawner2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        if (movingRight)
        {
            //If object is not farther than the starting pos + right travel dis,
            // it can move right
            if (transform.position.x <= startX + maxX)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else
            {
                movingRight = false;
            }
        }
        else
        {
            //If object is not farther than start pos + left travel dist.
            // it can move left
            if (transform.position.x >= startX + minX)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else
            {
                //If object goes too far left, move rghtwa
                movingRight = true;
            }
        }
        #endregion

        #region Turn on Side Spawn upon Spawning enough enemies
        if (enemiesSpawned >= level1Spawn && enemiesSpawned < level2Spawn)
        {
            sideSpawner1.SetActive(true);
            plane1.SetActive(false);
            
            UserInter.GetComponent<UIManager>().currentLevel = 2;
        }
        if (enemiesSpawned >= level2Spawn && enemiesSpawned < level3Spawn)
        {
            sideSpawner2.SetActive(true);
            plane2.SetActive(false);
            UserInter.GetComponent<UIManager>().currentLevel = 3;

        }
        if (enemiesSpawned >= level3Spawn)
        {
            //Turn off spawns & Summon Boss
            sideSpawner1.SetActive(false);
            sideSpawner2.SetActive(false);
            plane3.SetActive(false);
            canSpawn = false;
            UserInter.GetComponent<UIManager>().currentLevel = 4;

            //Activate Boss
            bossPrefab.SetActive(true);
        }
        #endregion
    }


    //IEnumerators
    private IEnumerator Spawner()
    {

        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn == true)
        {
            
            yield return wait;
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];
            enemiesSpawned++;
            tempEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            tempEnemy.GetComponent<EnemyMovement>().playerScore = playerShip;

        }

    }
}
