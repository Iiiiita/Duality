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
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        audioSource = GetComponent<AudioSource>();
        
    }
    private bool winnable = false;
    // Update is called once per frame
    void Update()
    {
        agent.destination = player.transform.position;
        if (playerController.hasDestroyedTestament == true)
        {
            winnable = true;
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // lose game
            // Destroy(other.gameObject);
            if (winnable == true)
            {
                gameManager.victoryPanel.SetActive(true);
            }
            else if (winnable == false)
            {
                Debug.Log("You interact with the paper, Has destroyed papers? " + winnable);
                audioSource.PlayOneShot(gameOverSound);
                gameManager.gameOverPanel.SetActive(true);
            }
            else
                Debug.Log("Lol ei toimi xd");

        }
    }
}
