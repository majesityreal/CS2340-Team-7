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
    [SerializeField] LevelBarStatus XPBar;
    [SerializeField] int level = 1;
    [SerializeField] int xp = 0;
    [SerializeField] int xpScalePerLevel = 100;
    int xpToNextLevel = 100;
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
        xpToNextLevel = xpScalePerLevel * level;
        XPBar.XPIndicator(xp, xpToNextLevel);
        XPBar.setLevel(level, xpToNextLevel-xp);
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
        XPBar.XPIndicator(xp, xpToNextLevel);
        XPBar.setLevel(level, xpToNextLevel-xp);
        
    }

    public void takeDamage(float damage)
    {
        currentHitpoints -= damage * (1 - damageReduction);
        if (currentHitpoints <= 0)
        {
            lives--;
            if (lives > 0)
            {
                Debug.Log("Extra life used!");
                currentHitpoints = maxHitpoints;
            }
            else
            {
                //Destroy(gameObject);
                Debug.Log("Player has died!");
                Time.timeScale = 0; // use this for pause too
            }
        }
        HPBar.HPIndicator(currentHitpoints, maxHitpoints);
    }

    public void Heal(float healAmount)
    {
        currentHitpoints += healAmount;
        if (currentHitpoints > maxHitpoints)
        {
            currentHitpoints = maxHitpoints;
        }
        HPBar.HPIndicator(currentHitpoints, maxHitpoints);
    }

    public void addXP(int xpToAdd)
    {
        xp += xpToAdd;
        CheckIfLevelUp();
    }

    public void CheckIfLevelUp()
    {
        if (xp >= xpToNextLevel)
        {
            level++;
            xp = 0;
            xpToNextLevel = level * xpScalePerLevel;
            Debug.Log("Level up!");
        }
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