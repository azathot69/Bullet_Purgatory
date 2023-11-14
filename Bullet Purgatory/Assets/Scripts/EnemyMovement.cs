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
    //Variables

    //How much HP the enemy has before dying
    public int health;

    //How fast the enemy moves
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
}
