using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/Dialogue", order = 1)]
    public class DialogueData : ScriptableObject
    {
        public List<string> lines;
    }
}