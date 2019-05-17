using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    // What Enemy will move towards
    GameObject player; 
    float playerHealth;
    Animator anim;

    // Reference to the navMashAgent
    UnityEngine.AI.NavMeshAgent nav;


    void Start ()
    {
        // Finds the player using its tag and gets its transform (location - where it is)
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <Player>().currentHealth;
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        anim = GetComponent<Animator>();
    }


    void Update ()
    {
                   
            if (this.GetComponent<Enemy>().isDead == false && playerHealth > 0)
            {
                nav.SetDestination(player.transform.position);
                anim.SetBool("IsAttack01", false);
                anim.SetBool("IsRun", true);
                anim.SetBool("IsIdle", false);
            }
            else
            {
                nav.enabled = false;
            }
       
    }
}
