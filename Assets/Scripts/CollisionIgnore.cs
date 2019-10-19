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
        string t = collision.collider.gameObject.tag;
        if (t != "Player" && t != "Body" && t != "Foot" && t != "Cannon")
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
    }
}
