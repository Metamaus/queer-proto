using System;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalData
{
    public class GameMemory : MonoBehaviour
    {
        public static GameMemory Instance;
        private List<string> BuiltConstructionIds = new ();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }

            Instance = this;
        }

        public bool IsBuilt(string id)
        {
            return BuiltConstructionIds.Contains(id);
        }

        public void Build(string id)
        {
            BuiltConstructionIds.Add(id);
        }

        public void ResetMemory()
        {
            BuiltConstructionIds = new ();
        }
    }
}