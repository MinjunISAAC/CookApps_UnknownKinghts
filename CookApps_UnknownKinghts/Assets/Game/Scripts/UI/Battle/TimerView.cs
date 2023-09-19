using InGame.ForBattle.ForTime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace InGame.ForBattle.ForUI
{
    public class TimerView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private TextMeshProUGUI _TMP_Timer = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Coroutine _co_Timer = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void PlayToTimer(float duration, Action doneCallBack)
        {
            if (_co_Timer == null)
                _co_Timer = StartCoroutine(_Co_Timer(duration, doneCallBack));
        }

        // ----- Private
        private string _Format(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);

            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------
        private IEnumerator _Co_Timer(float duration, Action doneCallBack)
        {
            var sec = 0f;

            _TMP_Timer.text = _Format(duration);

            while (sec < duration)
            {
                sec += Time.deltaTime * TimeScaler.GetValue();
                
                _TMP_Timer.text = _Format(duration - sec);

                yield return null;
            }

            _TMP_Timer.text = $"00:00";

            doneCallBack?.Invoke();
            _co_Timer = null;
        }

    }
}