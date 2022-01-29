using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject CreditsPanel;

    // Start is called before the first frame update
    void Start()
    {
        SettingsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        SettingsPanel.SetActive(true);
    }

    public void Credits()
    {
        CreditsPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        SettingsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
