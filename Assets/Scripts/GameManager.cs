using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}