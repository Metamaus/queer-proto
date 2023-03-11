using System.Collections;
using DefaultNamespace;
using GlobalData;
using UnityEngine;
using UnityEngine.AI;

namespace Interactions
{
    public class Buildable : MonoBehaviour
    {
        [SerializeField] private GameObject _initialStructure;
        [SerializeField] private GameObject _finalStructure;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioClip _buildingMusic;
        [SerializeField] private AudioClip _buildingSound;
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
            StartCoroutine(BuildStructures());
        }

        private IEnumerator BuildStructures()
        {
            _particleSystem.Play();
            SoundManager.Instance.StartMusic(_buildingMusic);
            SoundManager.Instance.PlayEffectSound(_buildingSound);
            yield return new WaitForSeconds(2);
            UpdateStructures();
            yield return new WaitForSeconds(1);
            _particleSystem.Stop();
            
            yield return new WaitForSeconds(3);
            // Todo: less dirty
            GameUI ui = FindObjectOfType<GameUI>();
            if (ui)
            {
                ui.FinishGame();
            }
        }

        private void UpdateStructures()
        {
            _initialStructure.SetActive(!Built);
            _finalStructure.SetActive(Built);
        }
    }
}