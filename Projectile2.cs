using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile2 : MonoBehaviour {
    GameObject[] enemiesHit;
    public float maxDamage = 50;
    public float minDamage = 30;
    public float damage;
    public bool attacked;
    List<GameObject> enemiesAttacked = new List<GameObject>();
  
    private void OnTriggerStay(Collider other)
    {
        //Checks to see if the target in the Sphere collider is already on the list and is an Enemy, if not, it adds it
        if (other.gameObject.tag == "Enemy" && !enemiesAttacked.Contains(other.gameObject)) 
        {
            enemiesAttacked.Add(other.gameObject);
            
        }
    }

    

    
    void Update () {
		if(!attacked && enemiesAttacked.Any())
        {
            foreach (Enemy stats in enemiesAttacked.Select(t => t.GetComponent<Enemy>()))
            {
                damage = Random.Range(minDamage, maxDamage);


                stats.health -= damage;
                
            }

        }
        attacked = true;
	}
}
