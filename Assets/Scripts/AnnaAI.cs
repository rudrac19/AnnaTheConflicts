using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class AnnaAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public float roamRadius = 5f;
    public float waitTime = 3f;
    public float waitTimer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {

        bool isWalking = agent.velocity.sqrMagnitude > 0.01f;
        animator.SetBool("isWalking", isWalking);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance){
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime) {
                waitTimer = 0f;
                ChooseNewDestination();
                
            }
        }

    }

    void ChooseNewDestination(){
        Vector3 randomDir = Random.insideUnitSphere * roamRadius;
        randomDir += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, roamRadius, NavMesh.AllAreas)) {
            agent.SetDestination(hit.position);
        }
    }
}
