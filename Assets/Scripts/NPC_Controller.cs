using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.Audio;

public class NPC_Controller : MonoBehaviour
{
    public Transform goal;
    public GameObject[] goalArray;
    public NavMeshAgent[] agents;
    public NavMeshAgent agent;
    public Transform playerPos;

    public GameObject playerGO;
    private bool isRecentlyRandomised = false;

    AudioSource audioSource;
    public AudioClip screamSound;

    // Start is called before the first frame update
    void Start()
    {
        PosRandomisation();

        audioSource = GetComponent<AudioSource>();
    }

    public void PosRandomisation()
    {

        int luku = Random.Range(0, goalArray.Length);

        agent = GetComponent<NavMeshAgent>();
        agent.destination = goalArray[luku].transform.position;
        isRecentlyRandomised = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGO.GetComponent<PlayerController>().isDetected == false && isRecentlyRandomised == false)
        {
            PosRandomisation();
        }
        if (playerGO.GetComponent<PlayerController>().isDetected == true && playerGO.GetComponent<PlayerController>().isRecentlyDetected == true)
        {
          //  audioSource.Pause();
            PlayerDetection();
            playerGO.GetComponent<PlayerController>().isRecentlyDetected = false;
        }
    }
    private float currentDistanceToPlayer;
    private float goalDistanceToPlayer = 15;
    public void PlayerDetection()
    {
        //  Debug.Log("SCREAM!!!");
        // agent.destination = playerPos.position;

        // PlayerDetection();

        //if (other.gameObject.CompareTag("Player"))
        //{

        // stamina consumption++
        //playerGO.GetComponent<PlayerController>().isDetected = true;
        //Debug.Log("Player Detected!");

        //Destroy(other.gameObject);

        /*if (playerGO.GetComponent<PlayerController>().isRecentlyDetected == true)
        {
            
        }*/
       /* else
            audioSource.UnPause();*/

      foreach (NavMeshAgent agent in agents)
     {
            agent.destination = playerPos.transform.position;

            if (!playerGO.GetComponent<PlayerController>().isDetected)
            {

                Debug.Log("Returning to duties");
                isRecentlyRandomised = false;
                PosRandomisation();
            }
            //agent.transform.position != playerPos.transform.position
            currentDistanceToPlayer = Vector3.Distance(playerPos.position, agent.transform.position);
            if (currentDistanceToPlayer >= goalDistanceToPlayer)
            {
               // audioSource.PlayOneShot(screamSound);
                agent.speed = 8;
                Debug.Log("Speedin'");
            }
            if (currentDistanceToPlayer < goalDistanceToPlayer)
            {
                agent.speed = 3.5f;
                Debug.Log("On the Spot");
                
                isRecentlyRandomised = false;
                PosRandomisation();
            }


        }


    }

}