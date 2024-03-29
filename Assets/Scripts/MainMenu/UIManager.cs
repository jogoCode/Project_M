using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _pause;
    [SerializeField] private FPSCamera _camera;

    private void Start()
    {
        if(_pause != null)
        {
            return;
        }
    }

    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void ChangeScene(int scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
        Debug.Log("OnApplicationQuit");
    }

    public void PauseGame()
    {

        if (Time.timeScale > 0f)
        {
            Time.timeScale = 0f;
            _pause.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        else
        {
            Time.timeScale = 1f;
            _pause.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
