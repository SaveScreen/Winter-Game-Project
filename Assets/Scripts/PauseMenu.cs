using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public bool gamepaused;
    public GameObject pauseMenuUI;
    public InputAction startbutton;
    private bool startbuttonpressed;

    void Start() {
        pauseMenuUI.SetActive(false);
        gamepaused = false;
    }

    void OnEnable() {
        startbutton.Enable();
    }
    void OnDisable() {
        startbutton.Disable();
    }

    // Update is called once per frame
    void Update(){
        startbuttonpressed = startbutton.IsPressed();
        if (startbuttonpressed) {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            } 
            
        }
        
    }

    public void Resume ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        gamepaused = false;
        startbutton.Enable();
    }

    void Pause ()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        gamepaused = true;
        startbutton.Disable();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
 }
