using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile3 : MonoBehaviour {

	
	
	

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<Rigidbody>().AddForce(0, 0, 500f);
        }
    }


}
