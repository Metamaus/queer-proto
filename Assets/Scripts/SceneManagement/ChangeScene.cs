using System;
using System.Collections;
using System.Collections.Generic;
using SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private Scenes _sceneToNavigateTo;
    [SerializeField] private SceneData _sceneData;
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
            return;
        
        // Todo(scene change): add black screen and delay
        SceneManager.LoadSceneAsync(_sceneData.GetScenePath(_sceneToNavigateTo));
    }
}
