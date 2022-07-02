using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rgb;
    Vector3 movementVect;
    [SerializeField] float maxHitpoints = 100;
    [SerializeField] float currentHitpoints = 100;
    [SerializeField] float playerSpeed = 5.0f;
    [SerializeField] float damageReduction = 0.0f; // percentage of damage taken mitagated by armor, 1.0f is invincible
    [SerializeField] int lives = 1;
    [SerializeField] HPBarStatus HPBar;
    Vector3 initialScaleOfSprite;
    float lastHorizontalVector;
    float lastVerticalVector;

    // Start is called before the first frame update
    void Awake()
    {
        rgb = GetComponent<Rigidbody2D>();
        movementVect = new Vector3();
        initialScaleOfSprite = transform.GetChild(0).localScale;
    }

    void Start() {
        lastHorizontalVector = 0f;
        lastVerticalVector = 0f;
    }

    // Update is called once per frame
    void Update()
    {   
        // flip character sprite if moving left or right
        if (movementVect.x < 0) // go left
        {
            //flip child sprite
            transform.GetChild(0).localScale = new Vector3(initialScaleOfSprite.x, initialScaleOfSprite.y, initialScaleOfSprite.z);
            // localScale = new Vector3(1, 1, 1);   
            //transform.localRotation = Quaternion.Euler(0, 0, 0);
            lastHorizontalVector = movementVect.x;
        }
        else if (movementVect.x > 0) // go right
        {
            transform.GetChild(0).localScale = new Vector3(-initialScaleOfSprite.x, initialScaleOfSprite.y, initialScaleOfSprite.z);
            //transform.localRotation = Quaternion.Euler(0, 0, 0);
            lastHorizontalVector = movementVect.x;
            
        }
        movementVect.x = Input.GetAxis("Horizontal"); // works with arrows and WASD
        movementVect.y = Input.GetAxis("Vertical");
        if (movementVect.y != 0)
        {
            lastVerticalVector = movementVect.y;
        }
        //transform.localRotation = Quaternion.Euler(0, 0, 0);
        movementVect = Vector3.ClampMagnitude(movementVect, 1.0f) * playerSpeed;
        //movementVect *= playerSpeed;
        rgb.velocity = movementVect;
        
    }

    public void takeDamage(float damage)
    {
        currentHitpoints -= damage * (1 - damageReduction);
        if (currentHitpoints <= 0)
        {
            lives--;
            if (lives == 0)
            {
                //Destroy(gameObject);
                Debug.Log("Player has died!");
                Time.timeScale = 0; // use this for pause too
            }
            else
            {
                Debug.Log("Extra life used!");
                currentHitpoints = maxHitpoints;
            }
        }
        HPBar.HPIndicator(currentHitpoints, maxHitpoints);
    }

    public float getLastHorizontalVector()
    {
        return lastHorizontalVector;
        //Debug.Log("lastHorizontalVector: " + lastHorizontalVector);
    }
    public float getLastVerticalVector()
    {
        return lastVerticalVector;
    }

}