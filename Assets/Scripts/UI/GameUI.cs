using System.Collections;
using System.Collections.Generic;
using SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private SceneData _sceneData; // todo: set data with scene initializer

    [SerializeField] private GameObject _pauseMenuObject;
    [SerializeField] private GameObject _gameUiObject;
    
    public void OpenMenu()
    {
        _gameUiObject.SetActive(false);
        _pauseMenuObject.SetActive(true);
    }
    
    public void ResumeGame()
    {
        _pauseMenuObject.SetActive(false);
        _gameUiObject.SetActive(true);
    }
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync(_sceneData.GetScenePath(Scenes.Menu));
    }
}
