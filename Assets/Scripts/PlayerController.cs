using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
     TODO:
     Konami Code
     Control Panel Handling
         */
    #region Movement Variables
    [SerializeField]
    private float moveSpeed = 20, jumpSpeed = 800, maxSpeed = 15, moveSpeedIncrement = 5;
    private float moveSpeedBase;
    private Rigidbody2D rBody;
    [SerializeField]
    private Transform[] legJoints = new Transform[2], kneeJoints = new Transform[2], ankleJoints = new Transform[2];
    [SerializeField]
    private bool grounded = false;
    private bool moveFirstLeg = true;
    private bool forwardStep = true;
    private float animationCounter = 0;
    private int legMaxRotation = 45; //negative is forwards, positive is backwards
    [SerializeField]
    private Transform[] groundCheck = new Transform[2];
    #endregion
    #region Attack Variables
    private int attackCooldown = 0, attackCooldownReset = 10, missileCooldown = 0, missileCooldownReset = 25, laserCooldown = 0, laserCooldownReset = 25, laserDamage = 2, laserDamageMax = 5;
    [SerializeField]
    private GameObject attackObj, missileObj, laserObj;
    private bool missile = false, laser = false;
    [SerializeField]
    private Transform attackSpawn, missileSpawn, laserSpawn, body;
    #region Propeties
    public int LaserDamage { get { return laserDamage; } }
    #endregion
    #endregion
    #region Player Stat Variables
    private int health = 100;
    private int maximumHealth = 100;
    private int powerCapsules = 0;
    private bool[] canUpgrade = new bool[3];
    #endregion

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        moveSpeedBase = moveSpeed;
        health = maximumHealth;
    }

    void Update()
    {

    }
    void FixedUpdate()
    {
        #region Move Player
        foreach (Transform t in groundCheck)
        {
            if (Physics2D.Linecast(transform.position, t.position, 1 << LayerMask.NameToLayer("Foreground")))
            {
                grounded = true;
                break;
            }
        }
        moveSpeed = grounded ? moveSpeedBase : moveSpeedBase/2.5f;
        float horiz = Input.GetAxis("Horizontal");
        float vert = grounded ? Input.GetAxis("Jump") : 0;
        rBody.AddForce(new Vector2(horiz * moveSpeed, vert * jumpSpeed));
        if (vert > 0)
        {
            grounded = false;
            #region Reset Animation
            moveFirstLeg = true;
            forwardStep = true;
            SetDefaultRotation();
            #endregion
        }
        if (grounded == false && rBody.velocity.magnitude > maxSpeed)
        {
            rBody.velocity = rBody.velocity.normalized*maxSpeed;
        }
        #region Animate Legs
        if (horiz != 0 && grounded)
        {
            int i = moveFirstLeg ? 0 : 1;
            float speed = -moveSpeed / 5;
            switch (forwardStep)
            {
                case true:
                    legJoints[i].Rotate(0, 0, speed);
                    ankleJoints[i].Rotate(0, 0, -speed);
                    MoveLegBackwards(1 - i, speed);
                    animationCounter += -speed;
                    if (animationCounter >= legMaxRotation)
                    {
                        forwardStep = false;
                        animationCounter = 0;
                    }
                    break;
                case false:
                    kneeJoints[i].Rotate(0, 0, -speed * 2);
                    ankleJoints[i].Rotate(0, 0, speed * 2);
                    MoveLegBackwards(1 - i, speed);
                    animationCounter += -speed;
                    if (animationCounter >= legMaxRotation)
                    {
                        forwardStep = true;
                        animationCounter = 0;
                        moveFirstLeg = moveFirstLeg ? false : true;
                    }
                    break;
            }
        }
        #endregion
        #endregion
        #region Attack
        if (Input.GetAxis("Fire1") > 0)
        {
            if (attackCooldown == 0)
            {
                Instantiate(attackObj, attackSpawn.position, body.rotation);
                attackCooldown = attackCooldownReset;
            }
            if (missile && missileCooldown == 0)
            {
                Instantiate(missileObj, missileSpawn.position, body.rotation);
                missileCooldown = missileCooldownReset;
            }
            if (laser && laserCooldown == 0)
            {
                Instantiate(laserObj, laserSpawn.position, body.rotation);
                laserCooldown = laserCooldownReset;
            }
        }
        #endregion
        #region Power Capsules
        if (Input.GetAxis("Fire2") > 0 && powerCapsules > 0)
        {
            if (powerCapsules == 1 && canUpgrade[0])
            {
                moveSpeed += moveSpeedIncrement;
                canUpgrade[0] = moveSpeed >= 2.5f * moveSpeedBase;
                powerCapsules = 0;
            }
            else if (powerCapsules == 2 && canUpgrade[1])
            {
                if (missile)
                {
                    missileCooldownReset -= 5;
                    canUpgrade[1] = missileCooldownReset == attackCooldownReset;
                }
                else
                {
                    missile = true;
                }
                powerCapsules = 0;
            }
            else if (powerCapsules == 3 && canUpgrade[2])
            {
                if (laser)
                {
                    ++laserDamage;
                    canUpgrade[2] = laserDamage == laserDamageMax;
                }
                else
                {
                    laser = true;
                }
                powerCapsules = 0;
            }
        }
        #endregion
        #region Cooldowns
        if (attackCooldown > 0)
        {
            --attackCooldown;
        }
        if (missileCooldown > 0)
        {
            --missileCooldown;
        }
        if (laserCooldown > 0)
        {
            --laserCooldown;
        }
        #endregion
    }
    private void SetDefaultRotation()
    {
        BodyController b = body.GetComponent<BodyController>();
        body.rotation = Quaternion.identity;
        b.ResetAngle();
        legJoints[0].rotation = Quaternion.identity;
        legJoints[1].rotation = Quaternion.identity;
        kneeJoints[0].rotation = Quaternion.identity;
        kneeJoints[0].Rotate(0, 0, -45);
        kneeJoints[1].rotation = Quaternion.identity;
        kneeJoints[1].Rotate(0, 0, -45);
        ankleJoints[0].rotation = Quaternion.identity;
        ankleJoints[0].Rotate(0, 0, -45);
        ankleJoints[1].rotation = Quaternion.identity;
        ankleJoints[1].Rotate(0, 0, -45);
        b.SetDirectionAndRotation();
    }
    private void MoveLegBackwards (int i, float speed)
    {
        legJoints[i].Rotate(0, 0, -speed / 2);
        kneeJoints[i].Rotate(0, 0, speed);
        ankleJoints[i].Rotate(0, 0, -speed / 2);
    }
}
