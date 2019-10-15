using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //NOTE: While moving fast, player will ocasionally fall through platforms

    //Public Variables
    /*public float speed = 10.00f;
    public float speedBoost;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public Sprite playerUp, playerDown, playerNormal;
    public GameObject weapon, missile, doubleMissile, laser, option, attackSpeed;
    public int weaponTimer, weaponReset;
    public int attackTimer, attackTimerReset;
    public Transform firePoint, missilePoint, doublePoint, attackSpeedSpawn1, attackSpeedSpawn2;
    public int attackSpeedTimer, attackSpeedTimerReset;

    //DO NOT EDIT!!!
    public GameObject[] options = new GameObject[2];
    public int optionsActive = 0;
    public GameObject[] attackSpeedSprites = new GameObject[2];
    //public Vector2[] previousPosition = new Vector2[2];

    //Private Variables
    private GameController game;
    private bool missileUpgrade = false, doubleUpgrade = false, laserUpgrade = false;
    private bool firing = false;
    private bool missileFired = false;
    private float x, y;
    */
    #region Movement Variables
    [SerializeField]
    private float moveSpeed = 20, jumpSpeed = 800, maxSpeed = 15, moveSpeedIncrement = 5;
    private float moveSpeedBase;
    private Rigidbody2D rBody;
    [SerializeField]
    private bool grounded = false;
    [SerializeField]
    private GameObject[] feet = new GameObject[2];
    [SerializeField]
    private Transform[] groundCheck = new Transform[2];
    #endregion
    #region Attack Variables
    private int attackCooldown = 0, attackCooldownReset = 10, missileCooldown = 0, missileCooldownReset = 25, laserCooldown = 0, laserCooldownReset = 25, laserDamage = 2, laserDamageMax = 5;
    [SerializeField]
    private GameObject attackObj, missileObj, laserObj;
    private bool missile = false, laser = false;
    private Transform attackSpawn, missileSpawn, laserSpawn, body;
    #endregion
    #region Player Stat Variables
    private int health = 100;
    private int maximumHealth = 100;
    private int powerCapsules = 0;
    private bool[] canUpgrade = new bool[3];
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        moveSpeedBase = moveSpeed;
        health = maximumHealth;
        /*game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        x = transform.position.x;
        y = transform.position.y;*/

        //previousPosition[0] = transform.position;
        //previousPosition[1] = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        /*
        //FIRE!!!
        if (Input.GetKeyDown(KeyCode.Z) && weaponTimer == 0)
        {
            firing = true;
            fireWeapon();
            //transform.position is the same as "where am I?"
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            firing = false;
        }
        //counter += Time.deltaTime; (Time.deltaTime is the time between now and last update)

        //Move Options
        if (transform.position.x != x || transform.position.y != y)
        {
            for (int z = 0; z < optionsActive; ++z)
            {
                options[z].GetComponent<OptionController>().moveOption(transform.position);
            }
        }
        x = transform.position.x;
        y = transform.position.y;*/
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
        }
        if (grounded == false && rBody.velocity.magnitude > maxSpeed)
        {
            rBody.velocity = rBody.velocity.normalized*maxSpeed;
        }
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
        /*
        //Get Player X and Y
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        //Move Player
        Vector2 p = transform.position;
        Vector2 v = rBody.velocity;
        Vector2 newVelocity = new Vector2(horiz, vert); //Vector 2 holds 2 variables, an X and a Y
        rBody.velocity = newVelocity * speed; //Move Player
        //Rocks Check
        //if (GameObject.FindGameObjectsWithTag("Rock").Length > 0)
        //{
            foreach (var i in GameObject.FindGameObjectsWithTag("Rock"))
            {
                if (GetComponent<Collider2D>().IsTouching(i.GetComponent<Collider2D>()))
                {
                rBody.position = new Vector2(
                                rBody.position.x,
            Mathf.Clamp(rBody.position.y, -3.6f, 4.2f)
                    );
                    //transform.position = p;
                    //rBody.velocity = -v;
                }
            }
        //}
        //previousPosition[1] = previousPosition[0];
        //previousPosition[0] = transform.position;

        //Prevent player from leaving screen
        rBody.position = new Vector2( //rBody.position = where the player is
            Mathf.Clamp(rBody.position.x, minX, maxX), //Value, Lowest Value, Highest Value, returns true or false
            Mathf.Clamp(rBody.position.y, minY, maxY)
            );

        //Set Player Animation
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) this.GetComponent<SpriteRenderer>().sprite = playerUp;
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) this.GetComponent<SpriteRenderer>().sprite = playerDown;
        else this.GetComponent<SpriteRenderer>().sprite = playerNormal;
        //Debug.Log($"{Input.key()}");


        //fire automatic attack
        if (attackTimer == 0 && firing)
        {
            fireWeapon();
        }

        //Control Panel Commands
        if (Input.GetKey(KeyCode.X) && game.powerCapsules > 0)
        {
            int capsules = game.powerCapsules;
            //Command 1 - Speed Up
            if (capsules == 1)
            {
                speed += speedBoost;
                upgradeCommands(false);
            }
            //Command 2 - Missile
            else if (capsules == 2 && missileUpgrade == false)
            {
                missileUpgrade = true;
                upgradeCommands(true);
            }
            //Command 3 - Double
            else if (capsules == 3 && doubleUpgrade == false)
            {
                doubleUpgrade = true;
                laserUpgrade = false;
                game.resetPanel(3, false);
                upgradeCommands(true);
            }
            //Command 4 - Laser
            else if (capsules == 4 && laserUpgrade == false)
            {
                doubleUpgrade = false;
                laserUpgrade = true;
                game.resetPanel(2, false);
                upgradeCommands(true);
            }
            //Command 5 - Option
            else if (capsules == 5 && optionsActive<2)
            {
                options[optionsActive] = Instantiate(option, transform.position, transform.rotation);
                ++optionsActive;
                if (optionsActive == 2)
                {
                    upgradeCommands(true);
                }
                else
                {
                    upgradeCommands(false);
                }
            }
            //Command 6 - Attack Speed
            else if (capsules == 6 && attackSpeedTimer == 0)
            {
                attackSpeedSprites[0] = Instantiate(attackSpeed, attackSpeedSpawn1.position, attackSpeedSpawn1.rotation);
                attackSpeedSprites[1] = Instantiate(attackSpeed, attackSpeedSpawn2.position, attackSpeedSpawn2.rotation);
                for (int z = 0; z < attackSpeedSprites.Length; ++z)
                {
                    attackSpeedSprites[z].transform.parent = transform;
                }
                attackSpeedTimer = attackSpeedTimerReset;
                upgradeCommands(true);
            }
        }
        if (weaponTimer > 0)
        {
            --weaponTimer;
        }
        if (attackTimer > 0)
        {
            --attackTimer;
        }
        if (attackSpeedTimer > 0)
        {
            --attackSpeedTimer;
            if (attackSpeedTimer == 0)
            {
                game.resetPanel(5, false);
                for (int z = 0; z < attackSpeedSprites.Length; ++z)
                {
                    Destroy(attackSpeedSprites[z]);
                }
            }
        }*/
    }
    /*private void upgradeCommands(bool wipe)
    {
        game.SFXPlay(game.upgradeActivate);
        game.resetPanel(game.powerCapsules - 1, wipe);
        game.powerCapsules = 0;
    }
    private void fireWeapon()
    {
        if (laserUpgrade)
        {
            Instantiate(laser, firePoint.position, firePoint.rotation); //Create instance of 'weapon' [Instantiate means "create an object"... or... "create an instance"]
            fireOptionWeapon(laser);
            game.SFXPlay(game.playerLaser);
        }
        else
        {
            Instantiate(weapon, firePoint.position, firePoint.rotation); //Create instance of 'weapon' [Instantiate means "create an object"... or... "create an instance"]
            fireOptionWeapon(weapon);
            game.SFXPlay(game.playerFire);
        }
        if (missileUpgrade && missileFired == false)
        {
            Instantiate(missile, missilePoint.position, missilePoint.rotation);
            fireOptionWeapon(missile);
            missileFired = true;
        }
        else if (missileFired == true)
        {
            missileFired = false;
        }
        if (doubleUpgrade)
        {
            Instantiate(doubleMissile, doublePoint.position, doublePoint.rotation);
            fireOptionWeapon(doubleMissile);
        }
        attackTimer = attackSpeedTimer>0? attackTimerReset/3 : attackTimerReset;
        weaponTimer = attackSpeedTimer > 0 ? weaponReset/3 : weaponReset;
    }
    private void fireOptionWeapon(GameObject optionWeapon)
    {
        for (int z = 0; z < optionsActive; ++z)
        {
            if (optionWeapon.name.Contains("Double"))
            {
                Instantiate(optionWeapon, options[z].transform.position, doublePoint.transform.rotation);
            }
            else
            {
                Instantiate(optionWeapon, options[z].transform.position, options[z].transform.rotation);
            }
        }
    }
    public void KonamiCode()
    {
        game.powerCapsules = 2;
        missileUpgrade = true;
        upgradeCommands(true);
        game.powerCapsules = 4;
        doubleUpgrade = false;
        laserUpgrade = true;
        game.resetPanel(2, false);
        upgradeCommands(true);
        int i = optionsActive;
        if (optionsActive < 2)
        {
            game.powerCapsules = 5;
            options[optionsActive] = Instantiate(option, transform.position, transform.rotation);
            ++optionsActive;
            if (optionsActive == 2)
            {
                upgradeCommands(true);
            }
            else
            {
                upgradeCommands(false);
            }
        }
        game.powerCapsules = 6;
        attackSpeedSprites[0] = Instantiate(attackSpeed, attackSpeedSpawn1.position, attackSpeedSpawn1.rotation);
        attackSpeedSprites[1] = Instantiate(attackSpeed, attackSpeedSpawn2.position, attackSpeedSpawn2.rotation);
        for (int z = 0; z < attackSpeedSprites.Length; ++z)
        {
            attackSpeedSprites[z].transform.parent = transform;
        }
        attackSpeedTimer = attackSpeedTimerReset;
        upgradeCommands(true);
        game.powerCapsules = 5;
    }*/
}
