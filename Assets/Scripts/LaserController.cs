using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : WeaponController
{
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().LaserDamage;
    }
}
