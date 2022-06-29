using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rgb;
    Vector3 movementVect;
    [SerializeField] float hitpoints = 100;
    [SerializeField] float playerSpeed = 5.0f;
    [SerializeField] float damageReduction = 0.0f; // percentage of damage taken mitagated by armor

    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        movementVect = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        // flip character sprite if moving left or right
        if (movementVect.x < 0) // go left
        {
            transform.localScale = new Vector3(1, 1, 1);   
            //transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (movementVect.x > 0) // go right
        {
            transform.localScale = new Vector3(-1, 1, 1);
            //transform.localRotation = Quaternion.Euler(0, 0, 0);
            
        }
        movementVect.x = Input.GetAxis("Horizontal"); // works with arrows and WASD
        movementVect.y = Input.GetAxis("Vertical");
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        movementVect *= playerSpeed;
        rgb.velocity = movementVect;
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // reduce hitpoints by enemy's damage accounting for damage reduction
            //hitpoints -= gameObject.GetComponent<EnemyController>().damage*(1-damageReduction);
            if (hitpoints <= 0)
            {
                Destroy(gameObject);
                Time.timeScale = 0; // use this for pause too
            }
        }
    }
}