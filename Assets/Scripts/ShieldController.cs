using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : EnemyController
{
    // Start is called before the first frame update
    protected override void SpecificStart() { }

    // Update is called once per frame
    protected override void SpecificUpdate() { }
    protected override void Move() { }

    void OnTriggerEnter2D(Collider2D collider)
    {
        string t = collider.gameObject.tag;
        if (t == "Player" || t == "Foot" || t == "Body" || t == "Cannon")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Damage(100);
        }
    }
}
