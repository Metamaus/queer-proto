using GlobalData;
using SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private Scenes _sceneToNavigateTo;
    [SerializeField] private SceneData _sceneData;
    [field: SerializeField] public Transform InitialPosition { get; private set; }

    public string TargetSceneName => _sceneData.GetScenePath(_sceneToNavigateTo);
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
            return;
        
        // Todo(scene change): add black screen and delay
        GameMemory.Instance?.ChangeScene();
        SceneManager.LoadSceneAsync(TargetSceneName);
    }
}
