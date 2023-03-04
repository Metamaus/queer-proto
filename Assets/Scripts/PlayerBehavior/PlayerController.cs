using System;
using System.Collections;
using System.Collections.Generic;
using Interactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private Interactable _currentInteractable;

    private void Awake()
    {
        _currentInteractable = null;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0)) // todo: block it with ui
        {
            if (_currentInteractable != null)
            {
                InteractWith(_currentInteractable);
                return;
            }
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Todo (navigation): ignore obstacles when clicking ?
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Walkable"))
                {
                    _navMeshAgent.isStopped = false; // todo(movement): block player movement better
                    _navMeshAgent.destination = hit.point;
                }
                else if (hit.collider.CompareTag("Interactable"))
                {
                    var interactable = hit.transform.gameObject.GetComponentInParent<Interactable>();
                    if (interactable)
                    {
                        Vector3 targetPosition = interactable.SetFocus(this, OnReachInteractable).position;
                        if (Vector3.Distance(targetPosition, transform.position) < _navMeshAgent.stoppingDistance)
                        {
                            InteractWith(interactable);
                        }
                        else
                        {
                            _navMeshAgent.destination = targetPosition;
                        }
                    }
                }
            }
        }
    }

    private void OnReachInteractable()
    {
        // todo(interaction) : rotate toward the target
    }

    public void InteractWith(Interactable interactable)
    {
        if (interactable.Interact())
        {
            _currentInteractable = null;
            return;
        }

        _currentInteractable = interactable;
    }
}
