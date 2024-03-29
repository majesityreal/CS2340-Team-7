using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Author:         Kevin Kwan
 *  Last Updated:   2022.07.06
 *  Version:        1.0
 */
 
public class cryTear : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 direction;
    [SerializeField] float speed = 1;
    [SerializeField] float damage = 1;
    [SerializeField] float overlayRadius = 0.5f;
    [SerializeField] STAudio sound;
    

    public void SetDirection(float x, float y)
    {
        direction = new Vector3(x, y, 0);
        if (direction.x > 0 && direction.y > 0)
        {
            // rotate the sprite to face the direction
            transform.rotation = Quaternion.Euler(0, 0, 225);
        } else if (direction.x < 0 && direction.y < 0) {
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (direction.x < 0 && direction.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 315);
        }
        else if (direction.x > 0 && direction.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 135);
        } else if (direction.x > 0)
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

        sound = FindObjectOfType<STAudio>();
    }

    bool hitDetected = false;
    void Update()
    {
        Vector3 finalDirection = Vector3.ClampMagnitude(direction, 1.0f) * speed;
        transform.position += finalDirection * Time.deltaTime;
        if (Time.frameCount % 6 == 0) {
            Collider2D[] thingsHit = Physics2D.OverlapCircleAll(transform.position, overlayRadius);
            foreach(Collider2D collision in thingsHit)
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
                sound.PlayShoot();
            }
        }
        if (Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) > 30)
        {
            Destroy(gameObject);
        }

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
}
