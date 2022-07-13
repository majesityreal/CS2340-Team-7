using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Author:         Kevin Kwan
 *  Last Updated:   2022.07.06
 *  Version:        1.0
 */
public class cryingTearsWeapon : MonoBehaviour
{
    [SerializeField] float attackDelay;
    float timer;
    PlayerController player;
    [SerializeField] GameObject projectile;
    // Start is called before the first frame update
    int dir_x = -1;
    int dir_y = 0;
    // to account for "scaling," the player will need to "upgrade" these by leveling up and selecting them weapons
    [SerializeField] float speed = 7;
    [SerializeField] float damage = 5;
    void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") < 0) {
            if (Input.GetAxis("Horizontal") < 0) {
                dir_x = -1;
                dir_y = -1;
            } else if (Input.GetAxis("Horizontal") > 0) {
                dir_x = 1;
                dir_y = -1;
            } else {
                dir_x = 0;
                dir_y = -1;
            }
        }
        else if (Input.GetAxis("Vertical") > 0) {
            if (Input.GetAxis("Horizontal") < 0) {
                dir_x = -1;
                dir_y = 1;
            } else if (Input.GetAxis("Horizontal") > 0) {
                dir_x = 1;
                dir_y = 1;
            } else {
                dir_x = 0;
                dir_y = 1;
            }
        }
        else if (Input.GetAxis("Horizontal") < 0) { 
            if (Input.GetAxis("Vertical") < 0) {
                dir_x = -1;
                dir_y = -1;
            } else if (Input.GetAxis("Vertical") > 0) {
                dir_x = -1;
                dir_y = 1;
            } else {
                dir_x = -1;
                dir_y = 0;
            }
        }
        else if (Input.GetAxis("Horizontal") > 0) {
            if (Input.GetAxis("Vertical") < 0) {
                dir_x = 1;
                dir_y = -1;
            } else if (Input.GetAxis("Vertical") > 0) {
                dir_x = 1;
                dir_y = 1;
            } else {
                dir_x = 1;
                dir_y = 0;
            }
        }
        if (timer < attackDelay)
        {
            timer += Time.deltaTime;
            return;
        }
        timer = 0;
        SpawnTear();
    }
    void SpawnTear()
    {
        GameObject tear = Instantiate(projectile, transform.position, Quaternion.identity);
        tear.GetComponent<cryTear>().SetDirection(dir_x, dir_y);
        if (GetComponentInParent<PlayerController>().getDamageScaleWithLevel())
        {
            tear.GetComponent<cryTear>().setDamage(damage * GetComponentInParent<PlayerController>().getdamageScale() * GetComponentInParent<PlayerController>().getLevel());
        }
        tear.GetComponent<cryTear>().setDamage(damage);
        tear.GetComponent<cryTear>().setSpeed(speed);
        // Debug.Log(tear.GetComponent<cryTear>().getSpeed());

        // change damage and speed here

    }
    
    public void setDamage(float damage)
    {
        this.damage = damage;
    }
    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
}
