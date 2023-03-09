using GlobalData;
using UnityEngine;
using UnityEngine.AI;

namespace Interactions
{
    public class Buildable : MonoBehaviour
    {
        [SerializeField] private GameObject _initialStructure;
        [SerializeField] private GameObject _finalStructure;
        [SerializeField] private string _id;

        public bool Built { get; private set; } // todo: keep built state when changing scenes
        
        private void Awake()
        {
            Built = GameMemory.Instance.IsBuilt(_id);
            UpdateStructures();
            //NavMeshBuilder.UpdateNavMeshData(); // todo: update navigation ?
        }

        public void Build()
        {
            if(Built)
                return;
            Built = true;
            GameMemory.Instance.Build(_id);
            UpdateStructures();
        }

        private void UpdateStructures()
        {
            _initialStructure.SetActive(!Built);
            _finalStructure.SetActive(Built);
        }
    }
}