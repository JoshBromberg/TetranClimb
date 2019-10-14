using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
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
        if (collision.gameObject.tag == "Rock" && transform.position.y > collision.gameObject.transform.position.y)
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
    }
}
