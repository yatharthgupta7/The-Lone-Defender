using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int health;
    public GameObject explosion;
    public float playerRange = 10f;

    public Rigidbody2D rb;

    public float moveSpeed;

    public bool shouldShoot;

    public float fireRate = 1f;

    private float shotCounter;
    public GameObject bullet;
    public Transform firePoint;

    public Animator anim;
    void Start()
    {
        
    }

    void Update()
    {
        if(Vector3.Distance(transform.position,PlayerController.Instance.transform.position)<playerRange)
        {
            Vector3 playerDirection = PlayerController.Instance.transform.position - transform.position;


            rb.velocity = playerDirection.normalized * moveSpeed;
            anim.SetBool("Attack",true);
            if(shouldShoot)
            {
                shotCounter -= Time.deltaTime;
                if(shotCounter<=0)
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    shotCounter = fireRate;
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Attack", false);
        }
    }

    public void TakeDamage()
    {
        health--;
        if(health<=0)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            AudioController.Instance.PlayEnemyDeath();
            PlayerController.Instance.enemyKilled++;
            PlayerController.Instance.KillAmt();
        }
        else
        {
            AudioController.Instance.PlayEnemyShot();
        }
    }

    private void OnCollisionEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            collision.transform.GetComponent<PlayerController>().TakeDamage(collision.transform.GetComponent<PlayerController>().currentHealth);
        }
    }
}
