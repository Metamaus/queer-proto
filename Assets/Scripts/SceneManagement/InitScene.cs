using UnityEngine;

public class InitScene : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _uiPrefab;
    [SerializeField] private Transform _playerStartPosition; // todo(change scene): change initial position depending on the previous position

    private void Awake() // Todo: manage these instances with don't destroy on load
    {
        GameObject playerObject = Instantiate(_playerPrefab, _playerStartPosition.position, Quaternion.identity);
        GameObject uiObject = Instantiate(_uiPrefab, Vector3.zero, Quaternion.identity);
        var canvases = uiObject.GetComponentsInChildren<Canvas>();
        foreach (var canvas in canvases) // dirty ?
        {
            canvas.worldCamera = Camera.main;
        }
    }
}
