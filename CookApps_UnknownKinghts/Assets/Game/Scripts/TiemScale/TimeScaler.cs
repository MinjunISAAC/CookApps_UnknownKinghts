using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForBattle.ForTime
{
    public static class TimeScaler
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private static TimeScale _timeSacle            = null;
        private static bool      _isLoadedDefaultValue = false;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public static void SetValue(float value)
        {
            _LoadToDefaultValue();
            _timeSacle.Set(value);
        }

        public static void RevertValue()
        {
            _LoadToDefaultValue();
            _timeSacle.Revert();
        }

        public static float GetValue()
        {
            _LoadToDefaultValue();
            return _timeSacle.Value;
        }

        // ----- Private
        private static void _LoadToDefaultValue()
        {
            if (_isLoadedDefaultValue)
                return;

            _isLoadedDefaultValue = true;

            _timeSacle = new TimeScale(1f);
        }
    }
}