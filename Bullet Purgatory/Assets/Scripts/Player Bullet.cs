using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
[Acuna, Joseph] [Hernandez, Max]
[12/06/23]
Controls the bullet's movement and lifespan
*/

public class PlayerBullet : MonoBehaviour
{
    public float speed = 5.0f;

    public bool facingForward = false;
    public bool facingBackward = false;
    public bool facingRight = false;
    public bool facingLeft = false;

    public float lifeSpan = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (facingForward == true)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }

        if (facingBackward == true)
        {
            transform.position -= Vector3.forward * speed * Time.deltaTime;
        }

        if (facingRight == true)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (facingLeft == true)
        {
            transform.position += Vector3.left* speed * Time.deltaTime;
        }
    }





    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(this.gameObject);
    }
}
