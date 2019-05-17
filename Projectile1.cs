using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile1 : MonoBehaviour {
    public Vector3 dir;
    public Rigidbody rb;
    public GameObject enemyAttacked, frames, mainParticle;
    public GameObject player;
    public float maxDamage = 20;
    public float minDamage = 10;
    public float damage;
    public bool dealtDamage;
    
   

    //References
    //Enemy zomb Enemy;

    AudioSource zombieAudio;
    



    private void Start()
    {

        player = GameObject.Find("Player");
       
            zombieAudio = GetComponent<AudioSource>();
        
    }

    void Update () {
		
	}

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy" && !dealtDamage)
        {
            dealtDamage = true;
            enemyAttacked = collision.gameObject;

            damage = Random.Range(minDamage, maxDamage);

            enemyAttacked.GetComponent<Enemy>().health -= damage;

            frames.transform.rotation = player.transform.rotation;

            if (enemyAttacked.GetComponent<Enemy>().isDead != true)
            {
                zombieAudio.Play();
            }

            

            // print("Damage dealt: " + damage);
            
            frames.SetActive(true);
            mainParticle.SetActive(false);
            
        } else if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Player")
        {
            frames.transform.rotation = player.transform.rotation;
            frames.SetActive(true);
            mainParticle.SetActive(false);
        }

    }
}