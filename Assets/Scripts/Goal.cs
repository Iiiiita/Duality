using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            NPC_Controller npcController = other.GetComponentInParent<NPC_Controller>();
            int luku = Random.Range(0, npcController.goalArray.Length);
            while (npcController.goalArray[luku].transform.position == gameObject.transform.position)
            {
                luku = Random.Range(0, npcController.goalArray.Length);
            }
            npcController.agent.destination = npcController.goalArray[luku].transform.position;
        }
    }
}
