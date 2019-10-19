using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetranArms : MonoBehaviour
{
    [SerializeField]
    private float i;
    private float rotation = 0, rotationIncrement;
    // Start is called before the first frame update
    void Start()
    {
        rotationIncrement = 90 / (50 / i);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -rotationIncrement);
    }
}
