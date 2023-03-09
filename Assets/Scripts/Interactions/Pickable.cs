using GlobalData;
using UnityEngine;

namespace Interactions
{
    public class Pickable : Interactable
    {
        [SerializeField] private GameObject visual;

        private void Start()
        {
            visual.SetActive(!GameMemory.Instance.FoundFlower);
        }

        public override bool Interact()
        {
            GameMemory.Instance.FoundFlower = true;
            visual.SetActive(false);
            return base.Interact();
        }
    }
}