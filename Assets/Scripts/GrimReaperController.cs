using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class GrimReaperController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public GameManager gameManager;
    public AudioClip gameOverSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.transform.position;
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // lose game
            // Destroy(other.gameObject);
            audioSource.PlayOneShot(gameOverSound); 
            gameManager.gameOverPanel.SetActive(true);
            
        }
    }
}
