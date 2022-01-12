using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damageAmt;

    public float bulletSpeed = 5f;

    private Rigidbody2D rb;

    private Vector3 direction;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = PlayerController.Instance.transform.position - transform.position;
        direction.Normalize();
        direction = direction * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController.Instance.TakeDamage(damageAmt);
            Destroy(gameObject);
        }
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
