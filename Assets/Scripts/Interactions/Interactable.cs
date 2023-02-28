﻿using System;
using UnityEngine;

namespace Interactions
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private Transform _interactionLocation;
        
        private bool _hasInteracted;
        private PlayerController _interactingEntity;
        private Action _onInteract;

        public Transform SetFocus(PlayerController player, Action onInteract = null)
        {
            if (onInteract != null)
            {
                _onInteract += onInteract;
            }
            _interactingEntity = player;
            
            return _interactionLocation;
        }

        private void UnFocus()
        {
            _interactingEntity = null;
        }

        public virtual void Interact()
        {
            print("[Interactable] Interact");
            _onInteract.Invoke();
            _onInteract = null; // is this okay ?
            UnFocus();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponentInParent<PlayerController>() == _interactingEntity) // todo(interactions): identify entity better
                Interact();
        }
    }
}