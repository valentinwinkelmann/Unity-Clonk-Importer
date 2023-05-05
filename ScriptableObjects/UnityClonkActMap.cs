using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace UnityClonk
{
    [CreateAssetMenu(fileName = "UnityClonk ActMap", menuName = "UnityClonk/UnityClonk ActMap", order = 1)]
    public class UnityClonkActMap : SerializedScriptableObject
    {
        public Dictionary<string, Sprite[]> Actions = new Dictionary<string, Sprite[]>();
    }
}