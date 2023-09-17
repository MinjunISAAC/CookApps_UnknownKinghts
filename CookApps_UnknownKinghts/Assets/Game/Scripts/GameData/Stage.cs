using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForChapterGroup
{
    [CreateAssetMenu(menuName = "Chapter Group/Create To Stage", fileName = "Stage")]
    public class Stage : ScriptableObject
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private int _step = 0;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public int Step => _step;
    }
}