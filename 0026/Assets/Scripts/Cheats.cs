using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            int _currentScene = SceneManager.GetActiveScene().buildIndex;
            int _nextScene = ++_currentScene;
            if (_nextScene == SceneManager.sceneCountInBuildSettings) 
            {
                _nextScene = 0;
            }
            SceneManager.LoadScene(_nextScene);
        }
    }
}
