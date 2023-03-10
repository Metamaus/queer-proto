using UnityEngine;

namespace DefaultNamespace.UI
{
    public class ButtonManager : MonoBehaviour
    {
        public void OnClick()
        {
            SoundManager.Instance.PlayOnClick();
        }
    }
}