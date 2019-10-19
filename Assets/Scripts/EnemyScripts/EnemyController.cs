using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    [SerializeField]
    private int health, damage, score;
    protected bool moving;
    protected Rigidbody2D rBody;
    [SerializeField]
    private GameObject deathAnimation;
    [SerializeField]
    private AudioClip deathSound, victory;
    [SerializeField]
    protected GameObject audioPlayer;
    public int Health { get { return health; } }


    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        SpecificStart();
    }

    protected abstract void SpecificStart();

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Move();
        }
        SpecificUpdate();
    }
    protected abstract void SpecificUpdate();
    public void Damage (int i)
    {
        health -= i;
        if (health <= 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddScore(score, deathAnimation, transform);
            audioPlayer.GetComponent<AudioSource>().clip = deathSound;
            Instantiate(audioPlayer, GameObject.FindGameObjectWithTag("AudioPlayer").transform);
            if (name.Contains("Core"))
            {
                Destroy(GameObject.FindGameObjectWithTag("Tetran"));
                GameObject g = GameObject.FindGameObjectWithTag("MainCamera");
                g.GetComponent<AudioSource>().clip = victory;
                g.GetComponent<AudioSource>().loop = false;
                g.GetComponent<AudioSource>().Play();
                g.GetComponent<CameraController>().Victory();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    protected abstract void Move();
    void OnTriggerEnter2D(Collider2D collider)
    {
        string t = collider.gameObject.tag;
        if (t == "Player" || t == "Foot" || t == "Body" || t == "Cannon")
        {
            int i = damage > 0 ? damage : health;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Damage(i);
            Destroy(gameObject);
        }
        else if (t == "Rock" && (name.Contains("Fire") || name.Contains("Zub")))
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }
}
