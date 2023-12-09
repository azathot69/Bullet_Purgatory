using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
[Acuna, Joseph] [Hernandez, Max]
[12/06/23]
Allows the player to be hit, move, and shoot bullets
*/
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

        //Go to game over if Lives <= 0
        if (lives <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    //Player collision
    public void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            default:
                break;

            case "Enemy Projectile":
                Debug.Log("A Bullet hit the player!!");
                other.gameObject.SetActive(false);
                Respawn();
                break;

            case "Enemy":
                Debug.Log("An Enemy hit the player!!");
                break;
        }
    }

    //Functions

    /// <summary>
    /// Controls the player's movements
    /// </summary>
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

        //Player fires bullet in direction of pressed key
        if (Input.GetKey("up"))
        {

            shootForward = true;
            shootBackward = false;
            shootRight = false;
            shootLeft = false;
            ShootBullet();
        }

        if (Input.GetKey("right"))
        {

            shootForward = false;
            shootBackward = false;
            shootRight = true;
            shootLeft = false;
            ShootBullet();
        }

        if (Input.GetKey("down"))
        {

            shootForward = false;
            shootBackward = true;
            shootRight = false;
            shootLeft = false;
            ShootBullet();
        }
        if (Input.GetKey("left"))
        {
            shootForward = false;
            shootBackward = false;
            shootRight = false;
            shootLeft = true;
            ShootBullet();
        }

        //Player shoots bomb
    }

    /// <summary>
    /// Removes a player's lives when hit with an enemy bullet
    /// </summary>
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
            SceneManager.LoadScene(2);
        }
    }

    /// <summary>
    /// Shoots player's bullets at various angles
    /// </summary>
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
