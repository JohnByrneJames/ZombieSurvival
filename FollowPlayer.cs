using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour {

    public NavMeshAgent agent;
    public Transform target;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update () {
        agent.SetDestination(target.position);
	}
}
