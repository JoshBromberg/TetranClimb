using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    //External Variables
    public float speedFactor;

    //Internal Variables - DO NOT EDIT!!!
    public bool fanTurned = false;
    public Vector2 speed;
    public bool fanTurnedUp = false;
    public int line;
    public int rugurrTimer = 0/*, rugurrTimerReset = 45*/;
    public int jump = 0, totalJumps = 0;

    // Start is called before the first frame update
    void Start()
    {
        //speed = new Vector2(speedFactor, 0);
        if (name.Contains("Spawn")) { }
        else if (name.Contains("Rashe"))
        {
            GameObject p;
            try
            {
                p = GameObject.FindGameObjectWithTag("Player");
                totalJumps = p.transform.position.y > transform.position.y ? 0 : 1;
            } catch (System.NullReferenceException) { }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (name.Contains("Spawn")) { }
        else if (name.Contains("Rashe"))
        {
            GameObject p;
            try
            {
                p = GameObject.FindGameObjectWithTag("Player");
                if (jump == 0 && ((totalJumps == 0 && p.transform.position.y <= transform.position.y) || (totalJumps == 1 && p.transform.position.y >= transform.position.y)))
                {
                    jump = 1;
                    speed.y = 0;
                    speed.x -= speedFactor;
                }
                else if (jump == 0)
                {
                    speed.y = p.transform.position.y > transform.position.y ? speedFactor : -speedFactor;
                    speed.x = -0.05f;
                }
            }
            catch (System.NullReferenceException)
            {
                if (jump == 0)
                {
                    jump = 1;
                    speed.y = 0;
                    speed.x -= speedFactor;
                }
            }
        }
    }
}
