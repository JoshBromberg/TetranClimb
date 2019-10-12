using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //Public Variables
    public GameObject straightFire, diagonalFire;
    public bool fired = false;

    //Private Variables
    private GameController game;
    private Transform player;
    private double margainOfError = 0.1;
    private Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        firePoint = transform.GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FiringPoint())
        {
            if (name.Contains("Rashe") && GetComponent<MoveLeft>().speed.x == 0) { }
            else
            {
                GameObject temp = Instantiate(straightFire, firePoint.position, firePoint.rotation);
                temp.GetComponent<MoveLeft>().speed.y = player.position.y > temp.transform.position.y ? temp.GetComponent<MoveLeft>().speed.y : -temp.GetComponent<MoveLeft>().speed.y;
                fired = true;
            }
        }
    }
    private bool FiringPoint ()
    {
        if (game.playerDeathTimer == 0 && fired == false) {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            /*if (diagonalFire && transform.position.x - player.position.x >= player.position.y - transform.position.y - margainOfError && transform.position.x - player.position.x <= player.position.y - transform.position.y + margainOfError)
            {
                return true;
            }
            else*/ if (/*diagonalFire == false && */transform.position.x >= player.position.x - margainOfError && transform.position.x <= player.position.x + margainOfError)
            {
                return true;
            }
        }
        return false;
    }
}
