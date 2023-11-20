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
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;

    private GameObject spawnedBullet;
    private float timer = 0f;
    private Vector3 playerPosition;
    private Vector3 startPosition;
    private bool canShoot = true;
    public int health;
    

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

            case SpawnerType.Aim:
                if (canShoot)
                {
                    StartCoroutine(AimShot(firingRate));
                }
                break;

            case SpawnerType.Triad:
                if (canShoot)
                {
                    StartCoroutine(TriadShot(firingRate));
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
            float bulletDirYPos = startPosition.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 bulletVector = new Vector3(bulletDirXPos, bulletDirYPos, 0);
            Vector3 bulletMoveDirection = (bulletVector - startPosition).normalized * bulletSpeed;

            GameObject tmpObj = Instantiate(bullet, startPosition, Quaternion.identity);

            tmpObj.GetComponent<Bullet>().bulletLife = bulletLife;
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirection.x, 0, bulletMoveDirection.y);



            angle += angleStep;
        }

        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private IEnumerator AimShot(float fireRate)
    {
        canShoot = false;

        Vector3 fireDirection = startPosition - playerPosition;

        float bulletDirXPos = playerPosition.x;
        float bulletDirYPos = playerPosition.z;

        //Main Bullet
        Vector3 bulletVector = new Vector3(bulletDirXPos, bulletDirYPos, 0);
        Vector3 bulletMoveDirection = (bulletVector - startPosition).normalized * bulletSpeed;

        GameObject tmpObj = Instantiate(bullet, startPosition, Quaternion.identity);

        tmpObj.GetComponent<Bullet>().bulletLife = bulletLife;
        tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirection.x, 0, bulletMoveDirection.y);


        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private IEnumerator TriadShot(float fireRate)
    {
        canShoot = false;

        Vector3 fireDirection = startPosition - playerPosition;

        float bulletDirXPos = playerPosition.x;
        float bulletDirYPos = playerPosition.z;

        //Main Bullet
        Vector3 bulletVector = new Vector3(bulletDirXPos, bulletDirYPos, 0);
        Vector3 bulletMoveDirection = (bulletVector - startPosition).normalized * bulletSpeed;

        GameObject tmpObjOne = Instantiate(bullet, startPosition, Quaternion.identity);

        tmpObjOne.GetComponent<Bullet>().bulletLife = bulletLife;
        tmpObjOne.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirection.x, 0, bulletMoveDirection.y);

        //Second Bullet
        Vector3 bulletVectorTwo = new Vector3(bulletDirXPos - 30, bulletDirYPos, 0);
        Vector3 bulletMoveDirectionTwo = (bulletVectorTwo - startPosition).normalized * bulletSpeed;

        GameObject tmpObjTwo = Instantiate(bullet, startPosition, Quaternion.identity);

        tmpObjTwo.GetComponent<Bullet>().bulletLife = bulletLife;
        tmpObjTwo.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirectionTwo.x, 0, bulletMoveDirectionTwo.y);

        //Third Bullet
        Vector3 bulletVectorThree = new Vector3(bulletDirXPos + 30, bulletDirYPos, 0);
        Vector3 bulletMoveDirectionThree = (bulletVectorThree - startPosition).normalized * bulletSpeed;

        GameObject tmpObjThree = Instantiate(bullet, startPosition, Quaternion.identity);

        tmpObjThree.GetComponent<Bullet>().bulletLife = bulletLife;
        tmpObjThree.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirectionThree.x, 0, bulletMoveDirectionThree.y);


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
