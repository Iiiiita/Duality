using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactions : MonoBehaviour
{
    public void DoorInteraction()
    {
        Debug.Log("You interact with the door");
        SceneManager.LoadScene(2);
    }

    public void PaperInteraction()
    {
        Debug.Log("You interact with the paper");

    }
}
