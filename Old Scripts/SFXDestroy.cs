using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXDestroy : MonoBehaviour
{
    public int destroyTimer = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        --destroyTimer;
        if (destroyTimer == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
