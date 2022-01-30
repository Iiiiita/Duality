using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public GameObject settingsPanel;
 
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        if (victoryPanel != null)
        { victoryPanel.SetActive(false); }
        settingsPanel.SetActive(false);
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    public void ClosePanel()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1;
    }


}
