using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Points variables
    public int PointsDiamonds,PointsCoins = 0;
    public int Points=0; //points 
    public Text DiamondPoints;
    public Text CoinsPoints;
    public int zombiesKilled;
    LayerMask groundLayer;




    //Variables
    private Rigidbody myRigidBody;
    public Rigidbody rigidbodyRef;
    public float movementSpeed, currentHealth, maxHealth, currentMana, maxMana, spell2Timer;
    public GameObject self, barrierRef, currentSpell, spawnRef;
    public Transform spellSpawn;
    public GameObject[] Spells, Enemies;
    public bool spell2Unlocked, spell3Unlocked,isDead;
    private bool attacking,attacking2, barrierActive;
    private float manaRegenTime = 2, _spell2Timer;
    public float fallingSpeed;
    public Camera cameraRef;




    //HUD Variables
    public Image currentHealthBar, currentManaBar;
    public Text ratioText;
    public Text ZombieCounter;
    public Text PointsText;

    

    //Movement Variables

    private Vector3 moveInput;
    private Vector3 moveVelocity;
    
   

    //Functions
     void Start()
    {
        groundLayer = LayerMask.GetMask("Ground"); 
        currentMana = maxMana;
        currentHealth = maxHealth;
        myRigidBody = GetComponent<Rigidbody>();
        currentSpell = Spells[0];
       
                
        //playerhurt = GetComponent<AudioSource>();
        //enemyScript = GetComponent<Enemy>();

    }

    void Update()
    {

        DiamondPoints.text = PointsDiamonds.ToString();
        CoinsPoints.text = PointsCoins.ToString();

        ZombieCounter.text = zombiesKilled.ToString();
        if(zombiesKilled>=0)
        {
            Points += zombiesKilled;
            if(Points > zombiesKilled*100)
            {
                Points = zombiesKilled * 100;
            }
            PointsText.text = Points.ToString();
        }

        

        //Creating of Array for character rotation
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance = 0.0f;

        //Setting cooldown for Spell 2
        if (attacking2)
        {
            _spell2Timer -= 1 * Time.deltaTime;
        }


        //Scaling of Health and Mana bars
        if(currentHealth>0)
        {
            float ratio = currentHealth / maxHealth; //value between 0 and 1
            currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
            ratioText.text = (ratio * 100).ToString("0") + '%';
        }
        else
        {
            currentHealthBar.rectTransform.localScale = new Vector3(0, 0, 0);
        }

        if (currentMana > 0)
        {
            float ratio = currentMana / maxMana; //value between 0 and 1
            currentManaBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        }
        else
        {
            currentManaBar.rectTransform.localScale = new Vector3(0, 0, 0);
        }

        if(currentMana>maxMana)
        {
            currentMana = maxMana;
        }
        if(currentHealth>maxHealth)
        {
            currentHealth = maxHealth;
        }


        //Updating of Mana regen cooldown
        if (attacking && manaRegenTime >= 0)
        {
            manaRegenTime -= 1 * Time.deltaTime;

        }

        //Regen of Mana
        if (manaRegenTime <= 0 && currentMana < maxMana && !barrierActive)
        {
            currentMana += 15 * Time.deltaTime;

            if (currentMana >= maxMana)
            {
                manaRegenTime = 2;
                attacking = false;
            }
          
        }

        //Decrease of mana for shield use
        if (barrierActive && currentMana >= 15)
        {
            currentMana = currentMana -= 15 * Time.deltaTime;
        } else if (currentMana < 15)
        {
            barrierRef.SetActive(false);
            barrierActive = false;
        }

        //Rotation of character to mouse
        if (!isDead)
        {

            if (playerPlane.Raycast(ray, out hitDistance))
            {
                Vector3 mousePosition = ray.GetPoint(hitDistance);
                Quaternion targetRotation = Quaternion.LookRotation(mousePosition - transform.position);
                transform.rotation = targetRotation;

                //Equipping the current spell
                if (Input.GetMouseButtonDown(0))
                {
                   
                    Attack();
                }

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    currentSpell = Spells[0];
                }

                if (Input.GetKeyDown(KeyCode.Alpha2) && spell2Unlocked)
                {
                    currentSpell = Spells[1];
                }

                if (Input.GetKeyDown(KeyCode.Alpha3) && spell3Unlocked)
                {
                    currentSpell = Spells[2];                   
                }

               if (Input.GetMouseButtonUp(0) && barrierActive)
                {
                    barrierRef.SetActive(false);
                    barrierActive = false;

                }

            }

            
        }
               
    }


    private void FixedUpdate()
    {
        fallingSpeed = myRigidBody.velocity.y;
        if (fallingSpeed > 0)
        {
            fallingSpeed = 0;
        }
        //Movement
        if (!isDead)
        {
                    
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), fallingSpeed, Input.GetAxisRaw("Vertical"));
            moveInput.Normalize();
            moveVelocity = moveInput * movementSpeed; 
            myRigidBody.velocity = moveVelocity;
        }
    }

    public void Attack()
    {
        if (!isDead)
        {
            //Other projectiles dont appear because they arent coded to the array element, see below
            //Attack = Spell one - Fireball.
            if (currentSpell.name == "Projectile1")
            {
                if (currentMana >= 10f)
                {
                    
                    
                    GameObject newSpell = Instantiate(currentSpell, spellSpawn.position, Quaternion.identity);

                    newSpell.GetComponent<Rigidbody>().AddForce(transform.forward * 100);


                    //Needed for all spells. Mana cost must be modified according to spell.
                    currentMana = currentMana - 10;
                    
                    Destroy(newSpell, 2.0f);
                    attacking = true;
                }


            } else if (currentSpell.name == "Projectile2" && _spell2Timer <= 0)
                //Attack = Spell two - Lightning Explosion
            {
                RaycastHit hit;
                Ray ray = cameraRef.ScreenPointToRay(Input.mousePosition);
                
                if (currentMana >= 30f)
                {
                    if (Physics.Raycast(ray, out hit, 100, groundLayer))
                    {
                        
                        GameObject newSpell = Instantiate(currentSpell, hit.point, Quaternion.identity);
                        currentMana = currentMana - 30;
                        attacking2 = true;
                        attacking = true;

                        Destroy(newSpell, 0.5f);
                        _spell2Timer = spell2Timer;

                    }

                }

                
            } else if (currentSpell.name == "Projectile3")
            {
                //Attack = Spell three - Lightning Wall
                if (currentMana >= 15f && !barrierActive)
                {

                    barrierRef.SetActive(true);
                    barrierActive = true;
                    attacking = true;
                }
            }
        }
    }

    public void Death()
    {
        isDead = true;
        GameObject.FindGameObjectsWithTag("Enemy");

        float Counter =+ Time.deltaTime;
        int i = 0;
        ratioText.text = i.ToString();
        


        Invoke("LoadTheScene",2);

        

    }

    void LoadTheScene()
    {
        SceneManager.LoadScene("Level 2");

    }


}