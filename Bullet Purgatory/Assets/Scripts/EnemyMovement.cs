using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
[Acuna, Joseph] [Hernandez, Max]
[12/06/23]
Allows the enemies to be hit, move, and shoot bullets
*/

public class EnemyMovement : MonoBehaviour
{

    #region Bullet Spawner Variables
    enum SpawnerType { Burst, Spin, DownShot, Triad }
    



    [Header("Bullet Atributes")]
    public GameObject bullet;
    public Transform player;
    public float bulletLife = 1f;
    public int bulletNum;
    public float bulletSpeed = 1f;
    private const float radius = 1f; //Find move direction
    #endregion

    #region Spawner Variables
    [Header("Spawner Atributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;

    private GameObject spawnedBullet;
    private float timer = 0f;
    private Vector3 playerPosition;
    private Vector3 startPosition;
    private bool canShoot = true;
    public int health;
    #endregion

    //The destination an enemy can reach before despawning
    public float killZ;

    public float killXRight;
    public float killXLeft;

    //Movement Variables
    [Header("Movement Atributes")]
    public int Movement = 1;
    public float speed; //How fast it moves
    public float minX;  //X Distance before moving other way
    public float maxX;
    public float minZ;  //Z Distance before moving other way
    public float maxZ;
    public bool movingRight; //If going Right
    public bool movingDown;
    public float startingX;

    //The amount the player recieves when they destroy this enemy.
    public int scoreValue;

    public GameObject playerScore;


    // Start is called before the first frame update
    void Start()
    {
        startingX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Location Update
        startPosition = transform.position;


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

            case SpawnerType.DownShot:
                if (canShoot)
                {
                    StartCoroutine(DownShot(firingRate));
                }
                break;

            case SpawnerType.Triad:
                if (canShoot)
                {
                    StartCoroutine(TriadShot(firingRate));
                }
                break;


        }

        switch (Movement)
        {
            default:
                Debug.Log("Uhoh");
                break;

            case 3:
                transform.position += Vector3.left * speed * Time.deltaTime;
                movingRight = false;
                break;

            case 2:
                transform.position += Vector3.right * speed * Time.deltaTime;
                movingRight = true;
                break;

            case 1:
                //Go all the way down
                transform.position -= Vector3.forward * speed * Time.deltaTime;
                movingDown = true;
                break;


        }

        

        //HP Depleted
        if (health <= 0)
        {
            Despawn();
            playerScore.GetComponent<PlayerMovement>().score += scoreValue;

        }

        if (transform.position.z <= killZ)
        {
            Despawn();
        }
        if (movingRight && transform.position.x >= killXRight)
        {
            Despawn();
        }
        if (!movingRight && transform.position.x <= killXLeft)
        {
            Despawn();
        }

    }

    //Collision
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Bullet":
                other.gameObject.SetActive(false);
                health--;
                break;

            default:
                break;
        }
    }


    //Functions

    /// <summary>
    /// Despawn when reaching 0 HP
    /// </summary>
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

            Vector3 bulletVector = new Vector3(bulletDirXPos, 0, bulletDirYPos);
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

        Vector3 fireDirection = startPosition - playerPosition;

        float bulletDirXPos = startPosition.x + Mathf.Sin((180 * Mathf.PI) / 180) * radius;
        float bulletDirYPos = startPosition.z + Mathf.Cos((180 * Mathf.PI) / 180) * radius;

        //Main Bullet
        Vector3 bulletVector = new Vector3(bulletDirXPos, 0, bulletDirYPos);
        Vector3 bulletMoveDirection = (bulletVector - startPosition).normalized * bulletSpeed;

        GameObject tmpObj = Instantiate(bullet, startPosition, Quaternion.identity);

        tmpObj.GetComponent<Bullet>().bulletLife = bulletLife;
        tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirection.x, 0, bulletMoveDirection.z);


        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private IEnumerator TriadShot(float fireRate)
    {
        canShoot = false;

        Vector3 fireDirection = startPosition - playerPosition;

        float bulletDirXPos = startPosition.x + Mathf.Sin((180 * Mathf.PI) / 180) * radius;
        float bulletDirYPos = startPosition.z + Mathf.Cos((180 * Mathf.PI) / 180) * radius;

        float bulletDirXPosOne = startPosition.x + Mathf.Sin((90 * Mathf.PI) / 180) * radius;
        float bulletDirYPosOne = startPosition.z + Mathf.Cos((90 * Mathf.PI) / 180) * radius;

        float bulletDirXPosTwo = startPosition.x + Mathf.Sin((270 * Mathf.PI) / 180) * radius;
        float bulletDirYPosTwo = startPosition.z + Mathf.Cos((270 * Mathf.PI) / 180) * radius;

        #region  Main Bullet
        Vector3 bulletVector = new Vector3(bulletDirXPos, 0, bulletDirYPos);
        Vector3 bulletMoveDirection = (bulletVector - startPosition).normalized * bulletSpeed;

        GameObject tmpObj = Instantiate(bullet, startPosition, Quaternion.identity);

        tmpObj.GetComponent<Bullet>().bulletLife = bulletLife;
        tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirection.x, 0, bulletMoveDirection.z);
        #endregion

        #region Second Bullet
        Vector3 bulletVectorOne = new Vector3(bulletDirXPosOne, 0, bulletDirYPosOne);
        Vector3 bulletMoveDirectionOne = (bulletVectorOne - startPosition).normalized * bulletSpeed;

        GameObject tmpObjOne = Instantiate(bullet, startPosition, Quaternion.identity);

        tmpObjOne.GetComponent<Bullet>().bulletLife = bulletLife;
        tmpObjOne.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirectionOne.x, 0, bulletMoveDirectionOne.z);
        #endregion

        #region Third Bullet
        Vector3 bulletVectorTwo = new Vector3(bulletDirXPosTwo, 0, bulletDirYPosTwo);
        Vector3 bulletMoveDirectionTwo = (bulletVectorTwo - startPosition).normalized * bulletSpeed;

        GameObject tmpObjTwo = Instantiate(bullet, startPosition, Quaternion.identity);

        tmpObjTwo.GetComponent<Bullet>().bulletLife = bulletLife;
        tmpObjTwo.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirectionTwo.x, 0, bulletMoveDirectionTwo.z);
        #endregion

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
            float bulletDirYPos = startPosition.z + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 bulletVector = new Vector3(bulletDirXPos, 0, bulletDirYPos);
            Vector3 bulletMoveDirection = (bulletVector - startPosition).normalized * bulletSpeed;

            GameObject tmpObj = Instantiate(bullet, startPosition, Quaternion.identity);

            tmpObj.GetComponent<Bullet>().bulletLife = bulletLife;
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirection.x, 0, bulletMoveDirection.z);



            angle = (angle) + angleStep;
            yield return new WaitForSeconds(firingRate);
        }


        //yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

}
