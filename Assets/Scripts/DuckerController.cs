using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckerController : MonoBehaviour
{
    //Public Variables
    public Sprite[,] DUCKERSprites = new Sprite[2, 3];
    public int red; //0 is off, 1 is on. It's not a bool because I need it as a number to refrence the DUCKERSprites
    public Sprite[] sprites = new Sprite[3];
    public Sprite[] redSprites = new Sprite[3];
    public int fired = 0, firedMaximum = 3;
    public bool rotateOnFire = true;
    public float speed = 0.08f;
    public int fireCooldown = 0, fireCooldownReset = 25;
    public bool reverseMovement = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int z=0; z<redSprites.Length; ++z)
        {
            DUCKERSprites[0, z] = sprites[z];
            DUCKERSprites[1, z] = redSprites[z];
        }
        if (red > 0)
        {
            gameObject.name += "Red";
        }
        if (reverseMovement)
        {
            speed = -speed;
        }
        GetComponent<MoveLeft>().speedFactor = GetComponent<MoveLeft>().speed.x = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fireCooldown > 0)
        {
            --fireCooldown;
        }
    }

    private void Update()
    {
        if (transform.position.y < 3) {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        bool stop = false;
        transform.position = new Vector3(transform.position.x, -3.7f, 0);
            for (int z = 0; z < enemies.Length && stop == false; ++z)
            {
                if (enemies[z].name.Contains("Mountain"))
                {
                    if (enemies[z].GetComponent<PolygonCollider2D>().bounds.Contains(transform.position))
                    {
                        for (float y = -3.7f; y < 3f && stop == false; y += 0.05f)
                        {
                            if (enemies[z].GetComponent<PolygonCollider2D>().bounds.Contains(new Vector2(transform.position.x, y))) { }
                            else
                            {
                                transform.position = new Vector3(transform.position.x, y, 0);
                                stop = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
