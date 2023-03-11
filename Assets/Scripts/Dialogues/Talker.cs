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
        [SerializeField] private string _talkerId;
        [SerializeField] private Animator _pnjAnimator;
        [SerializeField] private bool _isFriendly = true;

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
            _currentDialogue = GameMemory.Instance.GetTalkerMemory(_talkerId);
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
                GameMemory.Instance.UpdateTalker(_talkerId, _currentDialogue); // use a setter ?
                _currentLine = 0;
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