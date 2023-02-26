using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SceneManagement
{ 
    [Serializable]
    public enum Scenes
    {
        Menu,
        InitialScene,
        Gym1,
        Gym2
    }
    [CreateAssetMenu(fileName = "SceneData", menuName = "ScriptableObjects/SceneData", order = 1)]
    public class SceneData : ScriptableObject
    {
        [Serializable]
        private struct ScenePath
        {
            public Scenes sceneEnum;
            public String Path;
        }
        
        [SerializeField] private List<ScenePath> _scenesPaths;

        public string GetScenePath(Scenes sceneEnum)
        {
            foreach (var scenePath in _scenesPaths)
            {
                if (scenePath.sceneEnum == sceneEnum)
                {
                    return scenePath.Path;
                }
            }
            
            return String.Empty;
        }
    }
}