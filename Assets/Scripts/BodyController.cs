using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private bool reverse = false;
    private int factor = 768 / 2;
    public bool Reverse { get { return reverse; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x > factor && reverse == false)
        {
            player.Rotate(0, 180, 0);
            reverse = true;
        }
        else if (Input.mousePosition.x < factor && reverse)
        {
            player.Rotate(0, -180, 0);
            reverse = false;
        }
    }
}
