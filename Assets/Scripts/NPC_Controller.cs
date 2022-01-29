using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class NPC_Controller : MonoBehaviour
{
    public Transform goal;
    public GameObject[] goalArray;
    public NavMeshAgent[] agents;
    public NavMeshAgent agent;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        int luku = Random.Range(0, goalArray.Length);

        agent = GetComponent<NavMeshAgent>();
        agent.destination = goalArray[luku].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // stamina consumption++
            Destroy(other.gameObject);

            foreach (NavMeshAgent agent in agents)
            {
                agent.destination = player.transform.position;

                if(agent.transform.position != player.transform.position)
                {

                    agent.speed = 8;
                }
                if(agent.transform.position == player.transform.position)
                {
                    agent.speed = 0;
                } 
            }
        }
    }
}