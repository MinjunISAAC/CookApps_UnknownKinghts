using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForBattle.ForTime
{
    public class TimeScale
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private float _originValue = 1f;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public float Value
        {
            get;
            private set;
        } = 1.0f;

        // --------------------------------------------------
        // Constuctor
        // --------------------------------------------------
        public TimeScale(float value)
        {
            Value        = value;
            _originValue = value;
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void Set   (float value) { Value = value;        }
        public void Revert()            { Value = _originValue; }
    }
}