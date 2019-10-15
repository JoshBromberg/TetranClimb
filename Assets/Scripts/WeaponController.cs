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
        rBody.position += Vector2.left * speed;
        distanceTravelled += speed;
        if (distanceTravelled >= maxDistance)
        {
            rBody.position += Vector2.down * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cannon")
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().Damage(damage);
            if (destroyOnHit)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
