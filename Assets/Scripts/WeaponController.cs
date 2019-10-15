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
    private Vector2 baseMove = new Vector2(-1, 0);

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        //speed = GameObject.FindGameObjectWithTag("Body").GetComponent<BodyController>().Reverse ? -speed : speed;
    }

    // Update is called once per frame
    void Update()
    {
        rBody.position -= new Vector2 (transform.right.x, transform.right.y) * speed;
        distanceTravelled += speed;
        if (distanceTravelled >= maxDistance)
        {
            rBody.position -= new Vector2(0, transform.up.y) * speed;
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
