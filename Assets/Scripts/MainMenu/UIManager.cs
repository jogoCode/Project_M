using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject _pause;

    private void Start()
    {
        if(_pause != null)
        {
            return;
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
        PauseGame();
        }
    }
    public void ChangeScene(int scene)
    {
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

        }
        else
        {
            Time.timeScale = 1f;
            _pause.SetActive(false);
            AudioListener.volume = 1f;
        }
    }
}
