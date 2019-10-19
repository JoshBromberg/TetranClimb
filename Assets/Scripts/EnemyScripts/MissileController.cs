using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : EnemyController
{
    private float xSpeed = 0, ySpeed = 0;
    // Start is called before the first frame update
    protected override void SpecificStart()
    {
        Vector3 t;
        try
        {
            t = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        catch (System.NullReferenceException)
        {
            t = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        }
        float xDif = Mathf.Abs(transform.position.x - t.x), yDif = Mathf.Abs(transform.position.y - t.y);
        bool xIsCloser =  xDif > yDif;
        switch (xIsCloser)
        {
            case true:
                xSpeed = speed;
                ySpeed = yDif / xDif * speed;
                break;
            case false:
                ySpeed = speed;
                xSpeed = xDif / yDif * speed;
                break;
        }
        xSpeed = transform.position.x > t.x ? -xSpeed : xSpeed;
        ySpeed = transform.position.y > t.y ? -ySpeed : ySpeed;
        moving = true;
    }

    // Update is called once per frame
    protected override void SpecificUpdate()
    {
        
    }
    protected override void Move()
    {
        rBody.position += new Vector2(xSpeed, ySpeed);
    }
}
