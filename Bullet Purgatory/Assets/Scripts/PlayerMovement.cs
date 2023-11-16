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

        //Player goes down

        //Player goes left

        //Player goes right

        //Player fires bullet

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