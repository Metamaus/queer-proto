using System.Collections;
using System.Collections.Generic;
using GlobalData;
using SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneData _sceneData; // todo: place at a centralized space
    
    public void PlayGame()
    {
        if (GameMemory.Instance)
        {
            GameMemory.Instance.ResetMemory();
        }
        SceneManager.LoadSceneAsync(_sceneData.GetScenePath(Scenes.InitialScene));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
