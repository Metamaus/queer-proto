using DefaultNamespace;
using GlobalData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interactions
{
    public class Pickable : Interactable
    {
        [FormerlySerializedAs("visual")] [SerializeField] private GameObject _visual;
        [SerializeField] private AudioClip _soundOnPick;

        private void Start()
        {
            _visual.SetActive(!GameMemory.Instance.FoundFlower);
        }

        public override bool Interact()
        {
            GameMemory.Instance.FoundFlower = true;
            SoundManager.Instance.PlayEffectSound(_soundOnPick);
            _visual.SetActive(false);
            return base.Interact();
        }
    }
}