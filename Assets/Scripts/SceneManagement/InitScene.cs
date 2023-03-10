using System.Collections.Generic;
using DefaultNamespace;
using GlobalData;
using UnityEngine;

public class InitScene : MonoBehaviour
{
    [SerializeField] private GameObject _memoryPrefab;
    [SerializeField] private GameObject _soundPrefab;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _uiPrefab;
    [SerializeField] private Transform _playerStartPosition; // todo(change scene): change initial position depending on the previous position
    [SerializeField] private List<ChangeScene> _pathsToOtherScenes;

    private void Awake() // Todo: manage these instances with don't destroy on load
    {
        if (!GameMemory.Instance)
        {
            GameObject memory = Instantiate(_memoryPrefab, Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(memory);
        }

        if (!SoundManager.Instance)
        {
            GameObject soundManager = Instantiate(_soundPrefab, Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(soundManager);
        }
        
        Vector3 playerPosition = _playerStartPosition.position;
        Quaternion playerRotation = _playerStartPosition.rotation;
        foreach (var path in _pathsToOtherScenes)
        {
            if (path.TargetSceneName.Equals(GameMemory.Instance.PreviousScene))
            {
                playerPosition = path.InitialPosition.position;
                playerRotation = path.InitialPosition.rotation;
                break;
            }
        }
        Instantiate(_playerPrefab, playerPosition, playerRotation);
        
        GameObject uiObject = Instantiate(_uiPrefab, Vector3.zero, Quaternion.identity);
        var canvases = uiObject.GetComponentsInChildren<Canvas>();
        foreach (var canvas in canvases) // dirty ?
        {
            canvas.worldCamera = Camera.main;
        }

    }
}
