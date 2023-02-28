using System;
using UnityEngine;

namespace Interactions
{
    public class Buildable : MonoBehaviour
    {
        [SerializeField] private GameObject _initialStructure;
        [SerializeField] private GameObject _finalStructure;

        public bool Built { get; private set; }
        
        private void Awake()
        {
            Built = false;
            UpdateStructures();
        }

        public void Build()
        {
            if(Built)
                return;
            Built = true;
            UpdateStructures();
        }

        private void UpdateStructures()
        {
            _initialStructure.SetActive(!Built);
            _finalStructure.SetActive(Built);
        }
    }
}