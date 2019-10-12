using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZubController : MonoBehaviour
{
    public float speedFactor;
    public Sprite finalForm;
    public Vector2 speed;
    private Vector2 target;
    private Rigidbody2D rBody;
    private bool animatorDestroyed = false;
    private float xSpeed, ySpeed;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("GameController").GetComponent<BossRushController>().useDeathSpot ? GameObject.FindGameObjectWithTag("GameController").GetComponent<BossRushController>().playerDeathSpot.position : GameObject.FindGameObjectWithTag("Player").transform.position;

        //Set Speed
        bool maxSpeedHoriz = Mathf.Abs(rBody.position.x - target.x) > Mathf.Abs(rBody.position.y - target.y) ? true : false;
        if (maxSpeedHoriz)
        {
            xSpeed = rBody.position.x > target.x ? -speedFactor : speedFactor;
            ySpeed = (target.y - rBody.position.y) / ((target.x - rBody.position.x) / xSpeed);
        }
        else
        {
            ySpeed = rBody.position.y > target.y ? -speedFactor : speedFactor;
            xSpeed = (target.x - rBody.position.x) / ((target.y - rBody.position.y) / ySpeed);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Set Sprite
        if (GetComponent<SpriteRenderer>().sprite == finalForm && animatorDestroyed == false)
        {
            Destroy(GetComponent<Animator>());
            animatorDestroyed = true;
            speed = new Vector2(xSpeed, ySpeed);
        }

        //
    }
}
