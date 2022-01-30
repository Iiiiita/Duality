using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactions
{
    public bool hasDestroyedTestament;
    void Start() {
        hasDestroyedTestament = false;
    }
    public void DoorInteraction()
    {
        Debug.Log("You interact with the door");
        SceneManager.LoadScene(2);
    }

    public void PaperInteraction()
    {
        hasDestroyedTestament = true;
        Debug.Log("You interact with the paper, Has destroyed papers? " + hasDestroyedTestament);

    }
}
