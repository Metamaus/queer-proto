using UnityEngine;

public class InitScene : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerStartPosition; // todo(change scene): change initial position depending on the previous position
    
    private void Awake()
    {
        GameObject playerObject = Instantiate(_playerPrefab, _playerStartPosition.position, Quaternion.identity);
    }
}
