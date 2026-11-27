using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class AnnaAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public Transform playerPos;

    public float roamRadius = 5f;
    public float waitTime = 3f;
    public float waitTimer;
    public float rangeForFollow = 10f;
    public float rangeForAttack = 1f;


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

        /** we are checking a circle radius distance 
                so we can use the difference .sqrMagnitude 
                and compare it to square of the rangeForFollow */
        Vector3 diff = playerPos.position - transform.position;
        float sqrDiff = diff.sqrMagnitude;

        // check if it can attack player
        if (sqrDiff <= rangeForAttack * rangeForAttack){
            SceneSwitch.LoadScene("State", "gameover");

            Debug.Log("The State of the Game is: " + SceneSwitch.state);
        } else if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance){
            waitTimer += Time.deltaTime;

            if (sqrDiff <= rangeForFollow * rangeForFollow){
                agent.SetDestination(playerPos.position);
                Debug.Log("Player Found");
            } else if (waitTimer >= waitTime) {
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
