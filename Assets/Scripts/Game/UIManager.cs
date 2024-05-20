using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static bool isGamePause = false;
    public GameObject pausePanel;
    public GameObject Setting;
    public GameObject PauseBtn;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePause)
            {
                Resume();
                ResetOpt();
            }
            else
            {
                Pause();
            }
        }
    }
    
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isGamePause = false;
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isGamePause = true;
    }
    
    public void SaveGame()
    {
        Debug.Log("저장되었습니다.");
        SceneManager.LoadScene("MenuScene");
    }

    public void GameQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Settings()
    {
        Setting.SetActive(true);
        PauseBtn.SetActive(false);
    }

    public void ResetOpt()
    {
        Setting.SetActive(false);
        PauseBtn.SetActive(true);
    }
}
