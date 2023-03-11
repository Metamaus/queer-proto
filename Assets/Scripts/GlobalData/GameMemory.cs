using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GlobalData
{
    public class TalkerMemories
    {
        private List<string> TalkerIds = new ();
        private List<int> CurrentDialogues = new();

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

        public bool IsDialogueFinished(string talkerId, int dialogue)
        {
            if (talkerId.Equals(String.Empty))
            {
                return true;
            }
            
            for (int i = 0; i < TalkerIds.Count; i++)
            {
                if (talkerId.Equals(TalkerIds[i]))
                {
                    return CurrentDialogues[i] > dialogue;
                }
            }

            return false;
        }
    }
    public class GameMemory : MonoBehaviour
    {
        public static GameMemory Instance;
        private List<string> BuiltConstructionIds = new ();
        public readonly TalkerMemories TalkerMemories = new ();
        
        public bool FoundFlower = false;
        
        public string PreviousScene { get; private set; }

        public List<string> MovedCharacters;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }
            
            PreviousScene = string.Empty;
            Instance = this;
            MovedCharacters = new List<string>();
        }

        public int GetTalkerMemory(string talkerId)
        {
            return TalkerMemories.GetMemory(talkerId);
        }

        public void UpdateTalker(string talkerId, int newDialogue)
        {
            TalkerMemories.UpdateDialogue(talkerId, newDialogue);
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
            TalkerMemories.Reset();
        }

        public void ChangeScene()
        {
            PreviousScene = SceneManager.GetActiveScene().name;
        }

        public void MoveCharacter(string characterId)
        {
            MovedCharacters.Add(characterId);
        }
    }
}