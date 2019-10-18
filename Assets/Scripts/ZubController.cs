using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZubController : MissileController
{
    private int warmUpTimer = 25;
    protected override void SpecificStart()
    {
        
    }
    protected override void SpecificUpdate()
    {
        if (warmUpTimer > 0)
        {
            --warmUpTimer;
            if (warmUpTimer <= 0)
            {
                base.SpecificStart();
            }
        }
    }
}
