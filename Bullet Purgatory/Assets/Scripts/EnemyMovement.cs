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
    enum SpawnerType { Straight, Spin }

    [Header("Bullet Atributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float bulletSpeed = 1f;

    [Header("Spawner Atributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;

    private GameObject spawnedBullet;
    private float timer = 0f;


    //Enemy Variables


    public int health;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        switch (spawnerType)
        {
            case SpawnerType.Spin:
                transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
                break;
        }

        //Fire Bullet
        if (timer >= firingRate)
        {
            Fire();
            timer = 0;
        }

        //HP Depleted
        if (health <= 0)
        {
            Despawn();
        }

        
        
    }

    //Functions
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

    //Despawns itself
    private void Despawn()
    {
        gameObject.SetActive(false);
    }

    
}
