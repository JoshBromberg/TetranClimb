using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetranArms : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed;
    private float rotationIncrement;
    [SerializeField]
    private GameObject missile;
    private int missileCooldown, missileCooldownReset = 250;
    private Transform[] attackSpawn = new Transform[4];
    [SerializeField]
    private Transform parent;
    private Vector2 centre;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        rotationIncrement = 90 / (50 / spinSpeed);
        missileCooldown = missileCooldownReset;
        int a = 0;
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("AttackSpawn"))
        {
            attackSpawn[a] = g.transform;
            ++a;
        }
        centre = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -rotationIncrement);
        if (missileCooldown > 0)
        {
            --missileCooldown;
            if (missileCooldown <= 0)
            {
                foreach (Transform t in attackSpawn)
                {
                    Instantiate(missile, t.position, t.rotation);
                }
                missileCooldown = missileCooldownReset;
            }
        }
        angle += spinSpeed/20;

        Vector2 offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * 1f;
        parent.position = centre + offset;
        //parent.position += new Vector3(Mathf.Sin(movementTimer/50), Mathf.Cos(movementTimer/50));
    }
}
