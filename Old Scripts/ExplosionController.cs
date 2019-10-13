using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    //Public Variables
    public int explosionTimer;
    public string sound;
    public AudioClip mechanical, big, bigCore, volcano, normal;

    //Private Variables
    private GameController g;

    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        switch (sound.ToLower())
        {
            case "mechanical":
                g.SFXPlay(mechanical);
                break;
            case "big":
                g.SFXPlay(big);
                break;
            case "big core":
                g.SFXPlay(bigCore);
                break;
            case "volcano":
                g.SFXPlay(volcano);
                break;
            default:
                g.SFXPlay(normal);
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        --explosionTimer;
        if (explosionTimer == 0)
        {
            Destroy(gameObject);
        }
    }
}
