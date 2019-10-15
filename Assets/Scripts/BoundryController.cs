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
        if (collider.gameObject.tag == "Foot" || collider.gameObject.tag == "Cannon")
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2(0, 0.5f);
        }
        else
        {
            Destroy(collider.gameObject);
        }
    }
}
