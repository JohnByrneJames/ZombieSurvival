using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            other.GetComponent<Player>().PointsDiamonds++;
            Destroy(gameObject);
        }
    }

}
