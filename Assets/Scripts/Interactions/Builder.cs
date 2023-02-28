using UnityEngine;

namespace Interactions
{
    public class Builder : Interactable
    {
        [SerializeField] private Buildable _construction;

        public override void Interact()
        {
            base.Interact();
            _construction.Build();
        }
    }
}