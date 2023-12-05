using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SideSpawner : MonoBehaviour
{
    //Variables
    public float minZ = -33f;
    public float maxZ = 13f;
    public bool movingUp = true;
    public float speed = 10f;

    public float startZ;

    //How fast enemies spawn
    public float spawnRate;
    public bool canSpawn = true;
    public GameObject[] enemyPrefabs;
    public bool spawnRight;

    [SerializeField] private int enemiesSpawned;

    // Start is called before the first frame update
    void Start()
    {
        startZ = transform.position.z;
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        if (movingUp)
        {
            //If object is not farther than the starting pos + right travel dis,
            // it can move right
            if (transform.position.z <=  maxZ + startZ)
            {
                transform.position += Vector3.forward * speed * Time.deltaTime;
            }
            else
            {
                movingUp = false;
            }
        }
        else
        {
            //If object is not farther than start pos + left travel dist.
            // it can move left
            if (transform.position.z >=  minZ + startZ)
            {
                transform.position -= Vector3.forward * speed * Time.deltaTime;
            }
            else
            {
                movingUp = true;
            }
        }
        #endregion

        #region
        if (enemiesSpawned >= 5)
        {
            canSpawn = false;
        }
        #endregion
    }

    //Functions
    public void Switch(bool change)
    {
        switch (change)
        {
            case true:
                canSpawn = true;
                break;


            case false:
                canSpawn = false;
                break;
        }
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
            GameObject enemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            if (spawnRight)
            {
                //Make enemy go right
                enemy.GetComponent<EnemyMovement>().Movement = 2;
                enemy.transform.eulerAngles = new Vector3(0, 270, 0);
            }
            else
            {
                //Make enemy go left
                enemy.GetComponent<EnemyMovement>().Movement = 3;
                enemy.transform.eulerAngles = new Vector3(0, 90, 0);
            }

        }

    }
}
