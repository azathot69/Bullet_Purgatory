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

    //Number of lives player has
    public int lives;

    //Number of bombs player has
    public int bombs;

    public float minX = -52f;
    public float maxX = 52f;
    public float minY = -44f;
    public float maxY = 44f;

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
        }
    }

    //Functions
    private void Movement()
    {
        //Player goes up
        if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
        {

            //Moves the object over to the right w/ Vector3.right by speed (m/s)
            //Multiply that by Time.deltaTime to convert m/frame to m/s
            if (transform.position.y >= maxY)
            {

            }
            else
            {
                transform.position += Vector3.up * speed * Time.deltaTime;
            }

        }

        //Player goes down
        if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
        {

            //Moves the object over to the right w/ Vector3.right by speed (m/s)
            //Multiply that by Time.deltaTime to convert m/frame to m/s
            if (transform.position.y <= minY)
            {

            }
            else
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
            }

        }

        //Player goes left
        if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
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
        if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
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
        if (Input.GetKeyPushed(KeyCode.L))
        {
            Debug.Log("Player Shoots");
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
