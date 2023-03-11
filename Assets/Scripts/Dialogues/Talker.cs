using System;
using System.Collections.Generic;
using GlobalData;
using Interactions;
using TMPro;
using UnityEngine;

namespace Dialogues
{
    [Serializable]
    struct DialogueActions
    {
        public DialogueData data;
        public Buildable building;
        public string talkerConditionID;
        public int dialogueCondition;
        public bool flowerNeeded;
    }
    
    public class Talker : Interactable
    {
        [SerializeField] private List<DialogueActions> _dialogues; // could work the other way around, talkers are represented by enums and given their lines ?
        [SerializeField] private TextMeshProUGUI _dialogueTMP;
        [SerializeField] private GameObject _bubbleCanvas;
        [field: SerializeField] public string TalkerId { get; private set; }
        [SerializeField] private Animator _pnjAnimator;
        [SerializeField] private bool _isFriendly = true;
        [field: SerializeField] public bool IsFinal { get; private set; }

        public GameObject gameObject;
        private int _currentLine;
        private int _currentDialogue;
        private static readonly int Talk = Animator.StringToHash("Talk");

        private void Awake()
        {
            _currentLine = 0;
            _bubbleCanvas.SetActive(false);
        }

        private void Start()
        {
            _currentDialogue = GameMemory.Instance.GetTalkerMemory(TalkerId);
        }

        public override bool Interact()
        {
            base.Interact();
            if (_isFriendly && _currentLine == 0)
            {
                _pnjAnimator.SetTrigger(Talk);
            }
            
            if (_currentDialogue >= _dialogues.Count ||
                !GameMemory.Instance.TalkerMemories.IsDialogueFinished(
                    _dialogues[_currentDialogue].talkerConditionID, 
                    _dialogues[_currentDialogue].dialogueCondition) ||
                _dialogues[_currentDialogue].flowerNeeded && !GameMemory.Instance.FoundFlower)
            {
                // todo: play little sound and animation
                _bubbleCanvas.SetActive(false);
                return true;
            }
            if (_currentLine >= _dialogues[_currentDialogue].data.lines.Count)
            {
                _dialogues[_currentDialogue].building?.Build();
                _bubbleCanvas.SetActive(false);
                _currentDialogue++;
                GameMemory.Instance.UpdateTalker(TalkerId, _currentDialogue); // use a setter ?
                _currentLine = 0;

                if (_currentDialogue >= _dialogues.Count) // The game is about to end, all characters go to the final scen
                {
                    GameMemory.Instance.MoveCharacter(TalkerId);
                }
                return true;
            }
            
            // start dialogue
            _bubbleCanvas.SetActive(true);
            _dialogueTMP.text = _dialogues[_currentDialogue].data.lines[_currentLine];
            _currentLine++;
            return false;
        }
    }
}