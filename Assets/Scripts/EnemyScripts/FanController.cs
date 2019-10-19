using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : EnemyController
{
    private int direction = 1, checkTimer = 5, checkTimerReset = 5;
    [SerializeField]
    private Transform[] groundChecks = new Transform[2];
    protected override void SpecificStart()
    {
        moving = true;
    }
    protected override void SpecificUpdate() { }
    protected override void Move()
    {
        rBody.position += new Vector2(speed * direction, 0);
        if (checkTimer <= 0)
        {
            foreach (Transform t in groundChecks)
            {
                if (Physics2D.Linecast(transform.position, t.position, 1 << LayerMask.NameToLayer("Foreground"))) { }
                else
                {
                    direction = -direction;
                    checkTimer = checkTimerReset;
                    break;
                }
            }
        }
        else if (checkTimer > 0)
        {
            --checkTimer;
        }
    }
}
