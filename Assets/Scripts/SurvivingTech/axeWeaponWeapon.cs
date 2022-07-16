using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axeWeaponWeapon : MonoBehaviour
{
    [SerializeField] float attackDelay;
    float timer;
    PlayerController player;
    [SerializeField] GameObject projectile;
    // Start is called before the first frame update
    int dir_x = -1;
    // to account for "scaling," the player will need to "upgrade" these by leveling up and selecting them weapons
    [SerializeField] float speed = 7;
    [SerializeField] float damage = 5;
    [SerializeField] float gravity = 9.8f;
    [SerializeField] int quantity = 1;

    void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetAxis("Horizontal") < 0)
            {
                dir_x = -1;
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                dir_x = 1;
            }
           
        if (timer < attackDelay)
        {
            timer += Time.deltaTime;
            return;
        }
        timer = 0;
        for (int i = 0; i < quantity; i++)
        {
            SpawnTear();
        }
    }
    void SpawnTear()
    {
        GameObject tear = Instantiate(projectile, transform.position, Quaternion.identity);
        // makes the Y velocity a random value
        float randY = Random.Range(0.5f, 1.0f);
        float randX = Random.Range(0.8f, 1.2f);
        tear.GetComponent<axeWeapon>().SetDirection(dir_x * randX, randY);
        if (GetComponentInParent<PlayerController>().getDamageScaleWithLevel())
        {
            tear.GetComponent<axeWeapon>().setDamage(damage * GetComponentInParent<PlayerController>().getdamageScale() * GetComponentInParent<PlayerController>().getLevel());
            //Debug.Log(damage * GetComponentInParent<PlayerController>().getdamageScale() * GetComponentInParent<PlayerController>().getLevel());
            quantity = GetComponentInParent<PlayerController>().getLevel();
        } else {
            tear.GetComponent<axeWeapon>().setDamage(damage);
        }
        tear.GetComponent<axeWeapon>().setSpeed(speed);
        tear.GetComponent<axeWeapon>().setGravity(gravity);
        // Debug.Log(tear.GetComponent<cryTear>().getSpeed());

        // change damage and speed here

    }
}
