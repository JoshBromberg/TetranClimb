using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform[] legs = new Transform[2];
    private float previousAngle = 0;
    private bool reverse = false;
    public bool Reverse { get { return reverse; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        #region Rotation
        {
            int i = reverse ? -1 : 1;
            float currentAngle = reverse ?
                -1 * Mathf.Rad2Deg * Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x)
                :
                -1 * Mathf.Rad2Deg * Mathf.Atan2(mouse.x - transform.position.x, mouse.y - transform.position.y) - 90;
            if (currentAngle <= 22.5 && currentAngle >=-202.5)
            {
                transform.Rotate(0, 0, currentAngle - previousAngle);
                legs[0].Rotate(0, 0, -1 * (currentAngle - previousAngle));
                legs[1].Rotate(0, 0, -1 * (currentAngle - previousAngle));
                previousAngle = currentAngle;
            }
        }
        #endregion
        #region Direction
        if (mouse.x > transform.position.x && reverse == false)
        {
            player.Rotate(0, 180, 0);
            reverse = true;
        }
        else if (mouse.x < transform.position.x && reverse)
        {
            player.Rotate(0, -180, 0);
            reverse = false;
        }
        #endregion
        //Use Pythagorem Theorm to find the distance... but I need the angle...

    }
}
