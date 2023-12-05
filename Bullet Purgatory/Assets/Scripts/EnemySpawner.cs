using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    //Variables
    public float minX;
    public float maxX;
    private bool movingRight = true;
    public float speed = 10f;

    public float startX;

    //How fast enemies spawn
    public float spawnRate;
    public bool canSpawn = true;
    public GameObject[] enemyPrefabs;
    


    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        StartCoroutine(Spawner());
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

        #region

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

            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        }

    }
}
