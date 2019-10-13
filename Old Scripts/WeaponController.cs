using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //Public Variables
    public Vector2 Speed = new Vector2(0.5f, 0);
    public Sprite straightMissile;

    //Private Variables
    private Rigidbody2D rBody;
    private float reset = 5;
    private bool hit = false;
    private bool missileTurned = false;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        /*
         rBody.velocity = speed * transform.right; //Object's Right           moves item (1,0)*speed, also commands for up and forwards
         rBody.velocity = speed * Vector2.right;   //Right in general
         */
    }

    // Update is called once per frame
    void Update()
    {}

    private void FixedUpdate()
    {
        float horiz = rBody.position.x; //Figure out where the weapon is on the X-Axis
        float vert = rBody.position.y;
        rBody.position += Speed;
        if (horiz >= reset || vert >= reset || vert<= -reset)
        {
            Destroy(gameObject);
        }
        //Debug.Log($"{horiz}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (name.Contains("Missile"))
        {
            if (missileTurned == false && collision.gameObject.tag.Contains("Rock"))
            {
                Speed.y = 0;
                GetComponent<SpriteRenderer>().sprite = straightMissile;
                missileTurned = true;
            }
        }
        #region Enemy Handling
        if (hit == false)
        {
            GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
            if (collision.gameObject.tag.Contains("Weapon") || collision.gameObject.tag.Contains("Player")) { }
            else if (bosses.Length == 0)
            {
                if (collision.gameObject.name.Contains("Zub"))
                {
                    BossRushController game = GameObject.FindGameObjectWithTag("GameController").GetComponent<BossRushController>();
                    game.runDestructionCommands(gameObject, collision.gameObject);
                    if (name.Contains("Laser")) { }
                    else
                    {
                        hit = true;
                        Destroy(gameObject);
                    }
                }
                else
                {
                    EnemyCollisionCommands(collision);
                }
            }
            else
            {
                if (collision.gameObject.name.Contains("Projectile"))
                {
                    EnemyCollisionCommands(collision);
                }
                else if (collision.gameObject.tag.Contains("Enemy") || collision.gameObject.tag.Contains("Boss"))
                {
                    Destroy(gameObject);
                }
                else //Specific Boss Handling
                {
                    try
                    {
                        if (collision.gameObject.transform.parent.name.Contains("BigCore") || collision.gameObject.transform.parent.name.Contains("Big Core"))
                        {
                            bool red = collision.gameObject.transform.parent.GetComponent<BigCoreController>().redCore;
                            if (red == false)
                            {
                                GameObject[] cores = GameObject.FindGameObjectsWithTag("Core");
                                CoreController[] c = new CoreController[cores.Length];
                                for (int z = 0; z < cores.Length; ++z)
                                {
                                    c[z] = cores[z].GetComponent<CoreController>();
                                }
                                for (int z = 0; z < cores.Length; ++z)
                                {
                                    try
                                    {
                                        if (collision.gameObject == cores[z])
                                        {
                                            GameController g = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
                                            GameObject temp = Instantiate(c[z].explosion, cores[z].transform.position, cores[z].transform.rotation);
                                            temp.GetComponent<ExplosionController>().sound = "big core";
                                            g.UpdateScore(10000);
                                            g.backgroundMusicS.Stop();
                                            g.backgroundMusicS.clip = g.victory;
                                            g.backgroundMusicS.loop = false;
                                            g.backgroundMusicS.Play();
                                            Destroy(c[z].boss);
                                            foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                                            {
                                                Destroy(enemy);
                                            }

                                        }
                                        else if (collision.gameObject == c[z].shieldObjects[c[z].shieldObjects.Length - 1 - c[z].shieldsGone])
                                        {
                                            hit = true;
                                            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().UpdateScore(500);
                                            Destroy(collision.gameObject);
                                            ++c[z].shieldsGone;

                                        }
                                        else
                                        {
                                            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().SFXPlay(c[z].boss.GetComponent<BigCoreController>().hit);
                                        }
                                    }
                                    catch (System.IndexOutOfRangeException) { }
                                }
                            }
                        }
                    } catch (System.NullReferenceException) { }
                    Destroy(gameObject);
                }
            }
        }
        #endregion
    }
    private void EnemyCollisionCommands(Collider2D collision)
    {
        GameController game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (collision.gameObject.tag.Contains("Enemy"))
        {
            if (collision.gameObject.name.Contains("Fire") || collision.gameObject.name.Contains("Mountain")) { }
            else
            {
                try
                {
                    game.runDestructionCommands(gameObject, collision.gameObject);
                }
                catch (System.NullReferenceException) { }

                if (name.Contains("Laser")) { }
                else
                {
                    hit = true;
                    Destroy(gameObject);
                }
            }
        }
    }
}
