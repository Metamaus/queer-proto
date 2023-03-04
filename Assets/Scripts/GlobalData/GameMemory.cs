using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GlobalData
{
    class TalkerMemories
    {
        private List<string> TalkerIds = new ();
        private List<int> CurrentDialogues = new ();

        public void UpdateDialogue(string talkerId, int newDialogue)
        {
            for (int i = 0; i < TalkerIds.Count; i++)
            {
                if (talkerId.Equals(TalkerIds[i]))
                {
                    CurrentDialogues[i] = newDialogue;
                    return;
                }
            }
        }

        public int GetMemory(string talkerId)
        {
            for (int i = 0; i < TalkerIds.Count; i++)
            {
                if (talkerId.Equals(TalkerIds[i]))
                {
                    return CurrentDialogues[i];
                }
            }
            TalkerIds.Add(talkerId);
            CurrentDialogues.Add(0);
            return 0;
        }

        public void Reset()
        {
            TalkerIds = new();
            CurrentDialogues = new();
        }
    }
    public class GameMemory : MonoBehaviour
    {
        public static GameMemory Instance;
        private List<string> BuiltConstructionIds = new ();
        private readonly TalkerMemories _talkerMemories = new ();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }

            Instance = this;
        }

        public int GetTalkerMemory(string talkerId)
        {
            return _talkerMemories.GetMemory(talkerId);
        }

        public void UpdateTalker(string talkerId, int newDialogue)
        {
            _talkerMemories.UpdateDialogue(talkerId, newDialogue);
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
            _talkerMemories.Reset();
        }
    }
}