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

    enum SpawnerType {Burst, Spin, Aim, Triad}
    enum MoveType {PopIn, SideToSide, GoDown, GoSide }


    [Header("Bullet Atributes")]
    public GameObject bullet;
    public Transform player;
    public float bulletLife = 1f;
    public int bulletNum;
    public float bulletSpeed = 1f;
    private const float radius = 1f; //Find move direction

    [Header("Spawner Atributes")]
    public int health = 1;
    [SerializeField] private SpawnerType spawnerType;
    public float firingRate = 1f;
    private GameObject spawnedBullet;
    private float timer = 0f;
    private Vector3 playerPosition;
    private Vector3 startPosition;
    private bool canShoot = true;

    

    //Movement Variables
    [Header("Movement Atributes")]
    [SerializeField] private MoveType moveType;
    public float speed; //How fast it moves
    public float minX;  //X Distance before moving other way
    public float maxX;
    public float minZ;  //Z Distance before moving other way
    public float maxZ;
    public bool goingRight; //If going Right
    public bool retreat;    //If it returns



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Location Update
        startPosition = transform.position;
        playerPosition = player.transform.position;

        //Subtracting the 2 positions will have it aimed at that specific spot
        Vector3 fireDirection = startPosition - playerPosition; //Aim at player

        timer += Time.deltaTime;

        //Determine What Type of spawner
        switch (spawnerType)
        {
            case SpawnerType.Burst:
                if (canShoot)
                {
                    StartCoroutine(BurstShot(firingRate, bulletNum));
                }
                break;

            case SpawnerType.Spin:
                if (canShoot)
                {
                    StartCoroutine(SpinShot(firingRate, bulletNum));
                }
                break;

            case SpawnerType.FireDown:
                if (canShoot)
                {
                    StartCoroutine(DownShot(firingRate));
                }
                break;

            case SpawnerType.FireSide:
                if (canShoot)
                {
                    StartCoroutine(SideShot(firingRate));
                }
                break;


        }

        switch (moveType)
        {
            default:

                break;

            case MoveType.PopIn:

                break;

            case MoveType.SideToSide:

                break;

            case MoveType.GoDown:

                break;

            case MoveType.GoSide:

                break;
        }


        //HP Depleted
        if (health <= 0)
        {
            Despawn();
        }

        
        
    }

    //Functions

    //Despawns itself
    private void Despawn()
    {
        gameObject.SetActive(false);
    }

    //IEnumerators
    private IEnumerator BurstShot(float fireRate, int _bulletNum)
    {
        canShoot = false;
        float angleStep = 360f / _bulletNum; //Possible angles
        float angle = 0f;
        for (int i = 0; i <= _bulletNum - 1; i++)
        {
            //Direction Calculations
            float bulletDirXPos = startPosition.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float bulletDirYPos = startPosition.z + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
            Vector3 bulletVector = new Vector3(bulletDirXPos, bulletDirYPos, 0);
            Vector3 bulletMoveDirection = (bulletVector - startPosition).normalized * bulletSpeed;
            GameObject tmpObj = Instantiate(bullet, startPosition, Quaternion.identity);
            tmpObj.GetComponent<Bullet>().bulletLife = bulletLife;
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirection.x, 0, bulletMoveDirection.z);
            angle += angleStep;
        }
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private IEnumerator DownShot(float fireRate)
    {
        canShoot = false;

        GameObject tmpObj = Instantiate(bullet, startPosition, Quaternion.identity);

        tmpObj.GetComponent<Bullet>().bulletLife = bulletLife;
        tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0);

        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private IEnumerator SideShot(float fireRate)
    {
        canShoot = false;

        


        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private IEnumerator SpinShot(float fireRate, int _bulletNum)
    {
        canShoot = false;

        float angleStep = 360f / _bulletNum; //Possible angles
        float angle = 0f;

        for (int i = 0; i <= _bulletNum - 1; i++)
        {
            //Direction Calculations
            float bulletDirXPos = startPosition.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float bulletDirYPos = startPosition.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 bulletVector = new Vector3(bulletDirXPos, bulletDirYPos, 0);
            Vector3 bulletMoveDirection = (bulletVector - startPosition).normalized * bulletSpeed;

            GameObject tmpObj = Instantiate(bullet, startPosition, Quaternion.identity);

            tmpObj.GetComponent<Bullet>().bulletLife = bulletLife;
            //tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirection.x + playerPosition.x, 0, bulletMoveDirection.y + playerPosition.z); //This makes a cool pattern!
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirection.x, 0, bulletMoveDirection.y);



            angle = (angle) + angleStep;
            yield return new WaitForSeconds(firingRate);
        }


        //yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

}
