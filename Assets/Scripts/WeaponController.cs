using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private bool destroyOnHit;
    [SerializeField]
    private float speed = 0.1f;
    protected Rigidbody2D rBody;
    [SerializeField]
    protected int damage;
    private float distanceTravelled = 0;
    private float maxDistance = 5.1f;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rBody.position -= new Vector2 (transform.right.x, transform.right.y) * speed;
        distanceTravelled += speed;
        if (distanceTravelled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        string s = col.gameObject.tag;
        if (s == "Enemy")
        {
            col.gameObject.GetComponent<EnemyController>().Damage(damage);
            if (destroyOnHit || col.gameObject.name.Contains("Shield") || col.gameObject.name.Contains("Core"))
            {
                Destroy(gameObject);
            }
        }
        else if (s == "Rock" || s == "Arm" || s == "Tetran")
        {
            Destroy(gameObject);
        }
    }
}
