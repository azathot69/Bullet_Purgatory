using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Acuna, Joseph
/// [11/14/23]
/// Controls the player's movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    //Variables

    //Speed of player
    public float speed;

    //Rate of bullets fired
    public float fireRate;

    //Determine if player can shoot
    private bool canShoot = true;

    public bool shootForward = false;
    public bool shootBackward = false;
    public bool shootRight = false;
    public bool shootLeft = false;

    //Number of lives player has
    public int lives;

    public GameObject playerProjectilePrefab;
    public Transform shootingPoint;

    //Number of bombs player has
    public int bombs;

    //The player's score
    public int score = 0;

    public float minX = -52f;
    public float maxZ = 52f;
    public float minZ = -44f;
    public float maxX = 44f;

    //Position where the player respawns from death
    Vector3 spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Controls player movement
        Movement();
    }

    //Player collision
    public void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            default:
                Debug.Log("Something Hit the player!");
                break;

            case "Bullet":
                Debug.Log("A Bullet hit the player!!");
                Respawn();
                break;

            case "Enemy":
                Debug.Log("A Bullet hit the player!!");
                break;
        }
    }

    //Functions
    private void Movement()
    {
        //Player goes up
        if (Input.GetKey(KeyCode.W))
        {

            //Moves the object over to the right w/ Vector3.right by speed (m/s)
            //Multiply that by Time.deltaTime to convert m/frame to m/s
            if (transform.position.z >= maxZ)
            {

            }
            else
            {
                transform.position += Vector3.forward * speed * Time.deltaTime;
            }

        }

        //Player goes down
        if (Input.GetKey(KeyCode.S))
        {

            //Moves the object over to the right w/ Vector3.right by speed (m/s)
            //Multiply that by Time.deltaTime to convert m/frame to m/s
            if (transform.position.z <= minZ)
            {

            }
            else
            {
                transform.position -= Vector3.forward * speed * Time.deltaTime;
            }

        }

        //Player goes left
        if (Input.GetKey(KeyCode.A))
        {

            //Moves the object over to the right w/ Vector3.right by speed (m/s)
            //Multiply that by Time.deltaTime to convert m/frame to m/s
            if (transform.position.x <= minX)
            {

            }
            else
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }

        }

        //Player goes right
        if (Input.GetKey(KeyCode.D))
        {

            //Moves the object over to the right w/ Vector3.right by speed (m/s)
            //Multiply that by Time.deltaTime to convert m/frame to m/s
            if (transform.position.x >= maxX)
            {

            }
            else
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }

        }

        //Player fires bullet
        if (Input.GetKey("up"))
        {
            Debug.Log("Player Shoots");
            shootForward = true;
            shootBackward = false;
            shootRight = false;
            shootLeft = false;
            ShootBullet();
        }

        if (Input.GetKey("right"))
        {
            Debug.Log("Player Shoots");
            shootForward = false;
            shootBackward = false;
            shootRight = true;
            shootLeft = false;
            ShootBullet();
        }

        if (Input.GetKey("down"))
        {
            Debug.Log("Player Shoots");
            shootForward = false;
            shootBackward = true;
            shootRight = false;
            shootLeft = false;
            ShootBullet();
        }
        if (Input.GetKey("left"))
        {
            Debug.Log("Player Shoots");
            shootForward = false;
            shootBackward = false;
            shootRight = false;
            shootLeft = true;
            ShootBullet();
        }

        //Player shoots bomb
    }

    private void Respawn()
    {
        if (lives > 0)
        {
            //repawn player
            lives--;
        }
        else
        {
            //Go to game over screen
        }
    }

    //Fire a bomb, clears the screen
    private void FireBomb()
    {
        if (bombs > 0)
        {
            //Fire Bombs
            bombs--;
        }
        else
        {
            //Don't do Anything
            Debug.Log("no mo' bombs!");
        }
    }

    private void ShootBullet()
    {
        if (canShoot)
        {
            //Allow bullet to shoot
            
            //Bullet shooting
            if (shootForward == true)
            {
                GameObject playerProjectileInstance = Instantiate(playerProjectilePrefab, shootingPoint.position, shootingPoint.rotation);
                playerProjectileInstance.GetComponent<PlayerBullet>().facingForward = shootForward;
                StartCoroutine(Shooting(fireRate));
            }
            if (shootRight == true)
            {
                GameObject playerProjectileInstance = Instantiate(playerProjectilePrefab, shootingPoint.position, shootingPoint.rotation);
                playerProjectileInstance.GetComponent<PlayerBullet>().facingRight = shootRight;
                StartCoroutine(Shooting(fireRate));
            }
            if (shootBackward == true)
            {
                GameObject playerProjectileInstance = Instantiate(playerProjectilePrefab, shootingPoint.position, shootingPoint.rotation);
                playerProjectileInstance.GetComponent<PlayerBullet>().facingBackward = shootBackward;
                StartCoroutine(Shooting(fireRate));
            }
            if (shootLeft == true)
            {
                GameObject playerProjectileInstance = Instantiate(playerProjectilePrefab, shootingPoint.position, shootingPoint.rotation);
                playerProjectileInstance.GetComponent<PlayerBullet>().facingLeft = shootLeft;
                StartCoroutine(Shooting(fireRate));
            }
        }
    }

    private IEnumerator Shooting(float fireRate)
    {
        canShoot = false;
        //Fire Projectile


        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
