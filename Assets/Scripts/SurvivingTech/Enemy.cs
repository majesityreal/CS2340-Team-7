using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Author:         Kevin Kwan
 *  Last Updated:   2022.07.06
 *  Version:        1.0
 */
 
public class Enemy : MonoBehaviour
{
    [SerializeField] STAudio sound;
    
    Transform targetLocation;
    [SerializeField] GameObject targetPlayer;
    // let the spawner handle these values and account for wave scaling
    [SerializeField] float speed = 1;
    [SerializeField] float health = 50;
    [SerializeField] float damage = 5;
    [SerializeField] bool flip = true;
    //[SerializeField] int xp_amount = 10;
    Vector3 initialScaleOfSprite;

    Rigidbody2D rgbEnemy;

    void Awake()
    {
                sound = FindObjectOfType<STAudio>();
        rgbEnemy = GetComponent<Rigidbody2D>();
        initialScaleOfSprite = transform.localScale;
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
            if (flip)
            {
                transform.localScale = new Vector3(-initialScaleOfSprite.x, initialScaleOfSprite.y, initialScaleOfSprite.z); 
            }
        }
        else
        {
            transform.localScale = new Vector3(initialScaleOfSprite.x, initialScaleOfSprite.y, initialScaleOfSprite.z);
        }
        Vector3 direction = (targetLocation.position - transform.position).normalized;
        rgbEnemy.velocity = direction * speed;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // sound = FindObjectOfType<STAudio>();
        if (collision.gameObject == targetPlayer)
        {
            Harm();
            //sound.PlayDying();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision");
        if (collision.gameObject == targetPlayer)
        {
            sound.PlayDying();
        }
    }

    void Harm()
    {
        //sound = FindObjectOfType<STAudio>();
        if (targetPlayer != null)
        {
            targetPlayer.GetComponent<PlayerController>().takeDamage(damage);
            //sound.PlayDying();
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