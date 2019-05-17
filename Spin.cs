using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    private int spinx = 0;
    private int spiny = 2;
    private int spinz = 0;


    private void Update()
    {
        transform.Rotate(spinx, spiny, spinz);
    }
}
