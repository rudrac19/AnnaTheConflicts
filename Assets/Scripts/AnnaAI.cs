using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class AnnaAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public float roamRadius = 5f;
    public float waitTime = 3f;

    public Transform model;
    public Vector3 rotOffset = new Vector3(0f, 180f, 0f);

    public float waitTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance){
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime) {
                waitTimer = 0f;
                ChooseNewDestination();
                
            }
        }

        if (agent.velocity.sqrMagnitude > 0.01f)
        {
            Vector3 direction = agent.velocity.normalized;
            direction.y = 0;

            transform.rotation = Quaternion.LookRotation(direction);
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
