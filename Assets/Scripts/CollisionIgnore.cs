using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionIgnore : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.gameObject.tag=="Tetran" && collision.otherCollider.gameObject.tag == "Arm") || (collision.collider.gameObject.tag == "Arm" && collision.otherCollider.gameObject.tag == "Tetran"))
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
    }
}
