using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationFactor;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().angularVelocity = rotationFactor;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
