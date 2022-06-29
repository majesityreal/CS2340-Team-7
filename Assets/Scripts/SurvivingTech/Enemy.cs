using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform targetLocation;
    [SerializeField] GameObject targetPlayer;
    [SerializeField] float speed = 1;
    [SerializeField] float damage = 5;

    Rigidbody2D rgbEnemy;

    void Awake()
    {
        rgbEnemy = GetComponent<Rigidbody2D>();
        targetPlayer = targetLocation.gameObject;
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
        targetPlayer.GetComponent<PlayerController>().Hurt(damage);
    }
}
