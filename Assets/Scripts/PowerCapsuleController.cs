using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCapsuleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        string t = collider.gameObject.tag;
        if (t == "Player" || t == "Foot" || t == "Body" || t == "Cannon")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().AddPowerCapsule();
            Destroy(gameObject);
        }
    }
}
