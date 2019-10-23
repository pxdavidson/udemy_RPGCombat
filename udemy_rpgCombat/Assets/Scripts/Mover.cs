using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    // Start is called before the first frame update

    // Variables
    [SerializeField] Transform target;

    // Cache


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(target.position);
    }
}
