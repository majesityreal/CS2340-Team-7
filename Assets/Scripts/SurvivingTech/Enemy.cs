using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform targetLocation;
    [SerializeField] GameObject targetPlayer;
    // let the spawner handle these values and account for wave scaling
    [SerializeField] float speed = 1;
    [SerializeField] float health = 50;
    [SerializeField] float damage = 5;
    //[SerializeField] int xp_amount = 10;

    Rigidbody2D rgbEnemy;

    void Awake()
    {
        rgbEnemy = GetComponent<Rigidbody2D>();
    }

    public void setTarget(GameObject target)
    {
        targetPlayer = target;
        targetLocation = target.transform;
    }

    void FixedUpdate()
    {
        if (targetLocation.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        Vector3 direction = (targetLocation.position - transform.position).normalized;
        rgbEnemy.velocity = direction * speed;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetPlayer)
        {
            Harm();
        }
    }

    void Harm()
    {
        if (targetPlayer != null)
        {
            targetPlayer.GetComponent<PlayerController>().takeDamage(damage);
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //targetPlayer.GetComponent<PlayerController>().addXP(xp_amount);
            // spawn crystal for xp?
            Destroy(gameObject);
        }
    }

    //setters
    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
    public void setHealth(float health)
    {
        this.health = health;
    }
    public void setDamage(float damage)
    {
        this.damage = damage;
    }
}
