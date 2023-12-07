using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    //Variables
    public float speed;
    public int bossHealth;
    public int scoreValue;
    public Vector3 newRotation;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    public bool moving = false;
    public int spawnPos = 0;

    public GameObject playerScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement

        //Determine Spawn Point
        if (moving == false)
        {
            moving = true;
            SpawnPoint();
        }

        //Move
        Movement();

        //Change Position
        switch (spawnPos)
        {
            case 0:
                if (transform.position.x >= maxX)
                {
                    SpawnPoint();
                }
                break;

            case 1:
                if (transform.position.z >= maxZ)
                {
                    SpawnPoint();
                }
                break;

            case 2:
                if (transform.position.x <= minX)
                {
                    SpawnPoint();
                }
                break;

            case 3:
                if (transform.position.z <= minZ)
                {
                    SpawnPoint();
                }
                break;

        }

        //Shoot Bullets

        //Idea - Boss shoots more bullets/bullets become faster when low HP

        //Die when HP <= 0
        if (bossHealth <= 0)
        {
            Despawn();
            playerScore.gameObject.GetComponent<PlayerMovement>().score += scoreValue;
        }
    }

    //Collision
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Bullet":
                other.gameObject.SetActive(false);
                bossHealth--;
                break;

            default:
                break;
        }
    }

    private void Movement()
    {
        switch (spawnPos)
        {
            case 0: //Move right
                transform.position += Vector3.right * speed * Time.deltaTime;

                break;

            case 1: //Move Up
                transform.position += Vector3.forward * speed * Time.deltaTime;
                break; 
            
            
            case 2: //Move Left
                transform.position += Vector3.left * speed * Time.deltaTime;
                break;


            case 3: //Move Down
                transform.position -= Vector3.forward * speed * Time.deltaTime;


                break;


            default:



                break;
        }
    }

    private void SpawnPoint()
    {

        switch (Reroll())
        {
            case 0:
                Debug.Log("Upper West to East");
                newRotation = new Vector3(0, 270, 0);
                transform.eulerAngles = newRotation;
                transform.position = new Vector3(-57f, 0, 10);
                transform.eulerAngles = newRotation;
                spawnPos = 0;
                break;

            case 1:
                Debug.Log("Lower West to East");
                newRotation = new Vector3(0, 270, 0);
                transform.eulerAngles = newRotation;
                transform.position = new Vector3(-57f, 0, -17);
                spawnPos = 0;
                break;

            case 2:
                Debug.Log("Left South to North");
                newRotation = new Vector3(0, 180, 0);
                transform.eulerAngles = newRotation;
                transform.position = new Vector3(-29, 0, -56);
                spawnPos = 1;
                break;

            case 3:
                Debug.Log("Right South to North");
                newRotation = new Vector3(0, 180, 0);
                transform.eulerAngles = newRotation;
                transform.position = new Vector3(18, 0, -56);
                spawnPos = 1;
                break;

            case 4:
                Debug.Log("Upper East to West");
                newRotation = new Vector3(0, 90, 0);
                transform.eulerAngles = newRotation;
                transform.position = new Vector3(65, 0, -5);
                spawnPos = 2;
                break;

            case 5:
                Debug.Log("Lower East to West");
                newRotation = new Vector3(0, 90, 0);
                transform.eulerAngles = newRotation;
                transform.position = new Vector3(65, 0, -28);
                spawnPos = 2;
                break;

            case 6:
                Debug.Log("Left North to South");
                newRotation = new Vector3(0, 0, 0);
                transform.eulerAngles = newRotation;
                transform.position = new Vector3(-16, 0, 40);
                spawnPos = 3;
                break;

            case 7:
                Debug.Log("Right North to South");
                newRotation = new Vector3(0, 0, 0);
                transform.eulerAngles = newRotation;
                transform.position = new Vector3(29, 0, 37);
                spawnPos = 3;
                break;

            default:
                Debug.Log("Nothing's working!");
                break;

        }
    }


    private void Despawn()
    {
        this.gameObject.SetActive(false);

    }

    /// <summary>
    /// Randomizes boss's next position
    /// </summary>
    /// <returns></returns>
    private int Reroll()
    {
        int randizePos = Random.Range(0, 8);
        return randizePos;
}
}
