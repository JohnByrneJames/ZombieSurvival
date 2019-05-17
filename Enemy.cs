using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    // Variables
    public float movementSpeed;
    public float maxHealth = 100;
    public float health;
    Animator anim;


    private bool touchingPlayer;
    private GameObject player;
    public float attackTimer;
    private float _attackTimer;
    private bool attacked;
    private int animNumber;
    public float flashSpeed = 5f;

    AudioSource playerhurt;

   // public bool aggro;
    public float maxDamage;
    public float minDamage;
    public float damage;
    public bool isDead;

    public Image currentHealthBar;
    public Image damageImage;

    // References
    Player playerScript;

    Transform playerPosition;

    UnityEngine.AI.NavMeshAgent nav;

    // Functions, baby

    private void Start()
    {
        playerhurt = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        health = maxHealth;
        player = GameObject.FindWithTag("Player");
        _attackTimer = attackTimer;

        //playerScript = GetComponent<Player>();      // Player reference   
        //playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        //nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Update()
    {
        

        if (!isDead)
        {
            //Simply checks if player has aggroed the enemy
            
            FollowPlayer();

            


            if (health <= 0)
            {
                currentHealthBar.rectTransform.localScale = new Vector3(0, 0, 0);
                player.GetComponent<Player>().zombiesKilled += 1;
                animNumber = Random.Range(0, 3);
                if (animNumber == 0)
                {
                    anim.SetBool("IsAttack01", false);
                    anim.SetBool("IsRun", false);
                    anim.SetBool("IsIdle", false);

                    anim.Play("death01");

                } else if(animNumber == 1)
                {
                    anim.SetBool("IsAttack01", false);
                    anim.SetBool("IsRun", false);
                    anim.SetBool("IsIdle", false);

                    anim.Play("death02");

                } else if(animNumber == 2)
                {
                    anim.SetBool("IsAttack01", false);
                    anim.SetBool("IsRun", false);
                    anim.SetBool("IsIdle", false);

                    anim.Play("death03");
                    

                }

                isDead = true;
            }

            if (health > 0)
            {
                float ratio = health / maxHealth; //value between 0 and 1
                currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
                //ratioText.text = (ratio * 100).ToString("0") + '%';
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {//Lets system know that the enemy is in touching reach of the player.

        if (!isDead)
        {
            if (other.gameObject.tag == "Player")
            {
                playerhurt.Play(); // Plays the audio of playerhurt
                anim.SetBool("IsRun", false);
                anim.SetBool("IsIdle", false);
                touchingPlayer = true;


            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!isDead)
        {
            if (other.gameObject.tag == "Player")
            {
                anim.SetBool("IsAttack01", false);
                anim.SetBool("IsRun", true);
                anim.SetBool("IsIdle", false);
                touchingPlayer = false;
            }
        }
    }
    

    public void Attack()
    {
        if (!isDead)
        {
            //Attacking function
            if (!attacked)
            {
               
                
                damage = Random.Range(minDamage, maxDamage);

                

                player.GetComponent<Player>().currentHealth -= damage;


                anim.SetBool("IsAttack01", true);
                anim.SetBool("IsRun", false);
                anim.SetBool("IsIdle", false);                       
                
                
           
                attacked = true;

                

                if (player.GetComponent<Player>().currentHealth <= 0)
                {
                    player.GetComponent<Player>().currentHealth = 0;
                    player.GetComponent<Player>().Death();

                }
            } else if (attacked)
            {
                
                anim.SetBool("IsAttack01", false);
                anim.SetBool("IsRun", true);
                anim.SetBool("IsIdle", false);
            }
        }
               
    }


    //Function to follow player 
    public void FollowPlayer()
    {
        if (!isDead)
        {
            

            //Checks to see if it is able to attack again after cooldown.
            if (_attackTimer <= 0)
            {
                attacked = false;
                _attackTimer = attackTimer;
            }

            //Decreases cooldown once attacked.
            if (attacked)
            {
                
                _attackTimer -= 1 * Time.deltaTime;

            }

            //Resets cooldown
            if (!attacked)
            {
                
                _attackTimer = attackTimer;
            }

            //Initiates the attack function
            if (touchingPlayer)
            {
                Attack();

            }
        }
    }
}