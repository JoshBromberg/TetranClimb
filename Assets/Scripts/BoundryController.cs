using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundryController : MonoBehaviour
{
    public Vector2 playerSpawn = new Vector2(0, 0.5f);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        string t = collider.gameObject.tag;
        if (t == "Foot" || t == "Cannon" || t == "Player" || t == "Body")
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = playerSpawn;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
        else if (tag == "Enemy" || tag == "Weapon" || tag == "Rock")
        {
            Destroy(collider.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        OnTriggerExit2D(collision.collider);
    }
}
