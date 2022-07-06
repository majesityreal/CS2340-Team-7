using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform targetLocation;
    [SerializeField] GameObject targetPlayer;
    [SerializeField] float speed = 1;
    [SerializeField] float health = 50;
    [SerializeField] float damage = 5;
    [SerializeField] int xp_amount = 10;

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
            targetPlayer.GetComponent<PlayerController>().addXP(xp_amount);
            // spawn crystal for xp?
            Destroy(gameObject);
        }
    }
}
