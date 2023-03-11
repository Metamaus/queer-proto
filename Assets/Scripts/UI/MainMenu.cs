using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using GlobalData;
using SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneData _sceneData; // todo: place at a centralized space
    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _gameMusic;
    [SerializeField] private GameObject _soundManagerPrefab;
    [SerializeField] private GameObject _tutoObject;

    private void Awake()
    {
        if (!SoundManager.Instance)
        {
            GameObject soundManager = Instantiate(_soundManagerPrefab, Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(soundManager);
        }
        DisplayTutorial(false);
    }

    private void Start()
    {
        SoundManager.Instance.StartMusic(_menuMusic);
    }

    public void PlayGame()
    {
        if (GameMemory.Instance)
        {
            GameMemory.Instance.ResetMemory();
        }
        SoundManager.Instance.StartMusic(_gameMusic);
        SceneManager.LoadSceneAsync(_sceneData.GetScenePath(Scenes.InitialScene));
    }

    public void DisplayTutorial(bool display)
    {
        _tutoObject.SetActive(display);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
