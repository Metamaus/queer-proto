using DefaultNamespace;
using Interactions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private AudioClip _navigateClip;
    [SerializeField] private Animator _animator;

    private Interactable _currentInteractable;
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Interact = Animator.StringToHash("Interact");

    private void Awake()
    {
        _currentInteractable = null;
    }

    void Update()
    {
        if (!_navMeshAgent.hasPath)
        {
            _animator.SetBool(Walking, false);
        }
        
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
            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Walkable"))
                {
                    _animator.SetBool(Walking, true);
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
                            _animator.SetBool(Walking, true);
                            _navMeshAgent.destination = targetPosition;
                        }
                    }
                }
                else
                {
                    return;
                }
                
                SoundManager.Instance.PlayEffectSound(_navigateClip);
            }
        }
    }

    private void OnReachInteractable()
    {
        // todo(interaction) : rotate toward the target
    }

    public void InteractWith(Interactable interactable)
    {
        if (interactable is Pickable)
        {
            _animator.SetTrigger(Interact);
        }
        
        if (interactable.Interact())
        {
            _currentInteractable = null;
            return;
        }

        _currentInteractable = interactable;
    }
}
