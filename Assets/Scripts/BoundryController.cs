using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundryController : MonoBehaviour
{
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
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2(0, 0.5f);
        }
        else
        {
            Destroy(collider.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        OnTriggerExit2D(collision.collider);
    }
}
