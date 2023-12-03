using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    //Variables
    public float speed;
    public int bossHealth;
    public Vector3 newRotation;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement


        switch (Reroll())
        {
            case 0:
                Debug.Log("Upper West to East");
                newRotation = new Vector3(0, 270, 0);
                transform.eulerAngles = newRotation;
                transform.position = new Vector3(-57f, 0, 10);
                transform.eulerAngles = newRotation;
                break;

            case 1:
                Debug.Log("Lower West to East");
                newRotation = new Vector3(0, 270, 0);
                transform.eulerAngles = newRotation;
                transform.position = new Vector3(-57f, 0, -17);
                break;

            case 2:
                Debug.Log("Left South to North");
                newRotation = new Vector3(0, 180, 0);
                transform.eulerAngles = newRotation;
                transform.position = new Vector3(-29, 0, -56);
                break;

            case 3:
                Debug.Log("Right South to North");
                newRotation = new Vector3(0, 180, 0);
                transform.eulerAngles = newRotation;
                transform.position = new Vector3(18, 0, -56);

                break;

            case 4:
                Debug.Log("Upper East to West");
                newRotation = new Vector3(0, 90, 0);
                transform.eulerAngles = newRotation;
                transform.position = new Vector3(65, 0, -5);
                break;

            case 5:
                Debug.Log("Lower East to West");
                newRotation = new Vector3(0, 90, 0);
                transform.eulerAngles = newRotation;
                transform.position = new Vector3(65, 0, -28);
                break;

            case 6:
                Debug.Log("Left North to South");
                newRotation = new Vector3(0, 0, 0);
                transform.eulerAngles = newRotation;
                transform.position = new Vector3(-16, 0, 40);
                break;

            case 7:
                Debug.Log("Right North to South");
                newRotation = new Vector3(0, 0, 0);
                transform.eulerAngles = newRotation;
                transform.position = new Vector3(29, 0, 37);
                break;

            default:
                Debug.Log("Nothing's working!");
                break;
        }

        //Shoot Bullets

        //Idea - Boss shoots more bullets/bullets become faster when low HP

        //Die when HP <= 0
        if (bossHealth <= 0)
        {
            Despawn();
        }
    }

    private void Despawn()
    {
        this.gameObject.SetActive(false);

    }

    /// <summary>
    /// Randomizes boss's next position
    /// </summary>
    /// <returns></returns>
    private int Reroll()
    {
        int randizePos = Random.Range(0, 8);
        return randizePos;
}
}
