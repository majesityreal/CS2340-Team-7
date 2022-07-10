using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axeWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 direction;
    [SerializeField] float speed = 1;
    [SerializeField] float damage = 1;
    [SerializeField] float gravity = 9.8f;
    [SerializeField] float overlayRadius = 0.5f;

    float timePassed = 0.0f;


    public void SetDirection(float x, float y)
    {
        direction = new Vector3(x, y, 0);
        if (direction.x > 0 && direction.y > 0)
        {
            // rotate the sprite to face the direction
            transform.rotation = Quaternion.Euler(0, 0, 225);
        }
        else if (direction.x < 0 && direction.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (direction.x < 0 && direction.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 315);
        }
        else if (direction.x > 0 && direction.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 135);
        }
        else if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (direction.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (direction.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }


    }

    bool hitDetected = false;
    void Update()
    {
        Vector3 finalDirection = Vector3.ClampMagnitude(direction, 1.0f) * speed;
        finalDirection.y -= gravity * timePassed;
        transform.position += finalDirection * Time.deltaTime;
        if (Time.frameCount % 6 == 0)
        {
            Collider2D[] thingsHit = Physics2D.OverlapCircleAll(transform.position, overlayRadius);
            foreach (Collider2D collision in thingsHit)
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.takeDamage(damage);
                    hitDetected = true;
                    break;
                }
            }
            if (hitDetected)
            {
                Destroy(gameObject);
            }
        }
        if (Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) > 30)
        {
            Destroy(gameObject);
        }

        timePassed += Time.deltaTime;

    }

    public void setDamage(float damage)
    {
        this.damage = damage;
    }
    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
    public float getDamage()
    {
        return damage;
    }
    public float getSpeed()
    {
        return speed;
    }

    public void setGravity(float amount)
    {
        this.gravity = amount;
    }
}
