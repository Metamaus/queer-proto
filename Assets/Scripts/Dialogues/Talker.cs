using System;
using System.Collections.Generic;
using GlobalData;
using Interactions;
using TMPro;
using UnityEngine;

namespace Dialogues
{
    public class Talker : Interactable
    {
        [SerializeField] private List<DialogueData> _dialogues;
        [SerializeField] private TextMeshProUGUI _dialogueTMP;
        [SerializeField] private GameObject _bubbleCanvas;
        [SerializeField] private string _talkerId;

        private int _currentLine;
        private int _currentDialogue;

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
            if (_currentDialogue >= _dialogues.Count)
            {
                _bubbleCanvas.SetActive(false);
                return true;
            }
            if (_currentLine >= _dialogues[_currentDialogue].lines.Count) // todo: add a default answer ?
            {
                _bubbleCanvas.SetActive(false);
                _currentDialogue++;
                GameMemory.Instance.UpdateTalker(_talkerId, _currentDialogue); // use a setter ?
                _currentLine = 0;
                return true;
            }
            
            // start dialogue
            _bubbleCanvas.SetActive(true);
            _dialogueTMP.text = _dialogues[_currentDialogue].lines[_currentLine];
            _currentLine++;
            return false;
        }
    }
}