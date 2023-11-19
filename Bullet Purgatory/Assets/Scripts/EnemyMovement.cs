using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Acuna, Joseph
/// [11/14/23]
/// Controlls the enemy's behavior
/// </summary>
public class EnemyMovement : MonoBehaviour
{

    //Bullet Spawner Variables
    enum SpawnerType { Aim, Spin, SpinCounter, Triad , Straight}

    [Header("Bullet Atributes")]
    public GameObject bullet;
    public Transform player;
    public float bulletLife = 1f;
    public float bulletSpeed = 1f;

    [Header("Spawner Atributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;

    private GameObject spawnedBullet;
    private float timer = 0f;
    private float playerPositionX;
    private float playerPositionY;
    private float playerPositionZ;
    private Vector2 playerPosition;
    private Vector2 enemyPosition;


    //Enemy Variables


    public int health;
    public float speed;
    public float turnSpeed = 1f;
    public int turnMax = 90;
    public float aimDirection = 90f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Loaction Update
        /*
        playerPositionX = player.transform.position.x;
        playerPositionY = player.transform.position.y;
        playerPositionZ = player.transform.position.z;
        */
        enemyPosition = transform.position;
        playerPosition = player.transform.position;

        Vector2 fireDirection = enemyPosition - playerPosition;

        float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg - 90f;

        timer += Time.deltaTime;

        switch (spawnerType)
        {
            case SpawnerType.Spin:
                transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + turnSpeed);
                FireBullets();
                break;

            case SpawnerType.Aim:
                transform.eulerAngles = new Vector3(0f, 0f, angle - 90f);
                FireBullets();


                break;

            case SpawnerType.SpinCounter:
                transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z - turnSpeed);
                FireBullets();
                break;

                break;

            case SpawnerType.Triad:
                transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z - turnSpeed);
                if (transform.eulerAngles.z >= turnMax)
                {
                    transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }
                Debug.Log("Angle: " + transform.eulerAngles);
                FireBullets();
                break;

            case SpawnerType.Straight:
                transform.eulerAngles = new Vector3(0f, 0f, aimDirection);
                FireBullets();
                break;
        }

        //Fire Bullet
        

        //HP Depleted
        if (health <= 0)
        {
            Despawn();
        }

        
        
    }

    //Functions
    private void FireBullets()
    {
        if (timer >= firingRate)
        {
            Fire();
        timer = 0;
        }
    }

    private void FireThreeWay()
    {
        if (timer >= firingRate)
        {
            FireTriad();
            
            timer = 0;
        }
    }

    //Spawn Bullets
    private void Fire()
    {

        if (bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Bullet>().speed = bulletSpeed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;

        }
    }

    private void FireTriad()
    {

        if (bullet)
        {
            for (int i = 0; i <= 3; i++)
            {
                spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                spawnedBullet.GetComponent<Bullet>().speed = bulletSpeed;
                spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
                spawnedBullet.transform.rotation = transform.rotation;
            }


        }
    }

    //Despawns itself
    private void Despawn()
    {
        gameObject.SetActive(false);
    }

    
}
