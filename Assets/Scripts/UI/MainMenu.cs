using System.Collections;
using System.Collections.Generic;
using SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneData _sceneData; // todo: place at a centralized space
    
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(_sceneData.GetScenePath(Scenes.InitialScene));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
