using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsShoot : MonoBehaviour
{
    enum SpawnerType { Burst, Spin, DownShot, Triad }
    #region
    [Header("Bullet Atributes")]
    public GameObject bullet;
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
    private Vector3 startPosition;
    private bool canShoot = true;

    #endregion

    // Start is called before the first frame update
    void Start()
    {

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
        tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(-bulletMoveDirection.x, 0, bulletMoveDirection.z);
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
