// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

namespace Utiltiy.ForLoader
{
    public class Loader : MonoBehaviour
    {
        // --------------------------------------------------
        // Singleton
        // --------------------------------------------------
        // ----- Constructor
        private Loader() { }

        // ----- Static Variables
        private static Loader _instance = null;

        // ----- Variables
        private const string FILE_PATH = "Loader";
        private bool _isSingleton = false;

        // ----- Property
        public static Loader Instance
        {
            get
            {
                if (null == _instance)
                {
                    var existingInstance = FindObjectOfType<Loader>();

                    if (existingInstance != null)
                    {
                        _instance = existingInstance;
                    }
                    else
                    {
                        var origin = Resources.Load<Loader>(FILE_PATH);

                        _instance = Instantiate<Loader>(origin);
                        _instance._isSingleton = true;
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }

                return _instance;
            }
        }

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private Animation _animation = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const string SHOW_TRIGGER = "Loader_Show";
        private const string IDLE_TRIGGER = "Loader_Idle";
        private const string HIDE_TRIGGER = "Loader_Hide";

        // ----- Private
        private Coroutine _co_visiable = null;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Awake()
        {
            if (null == _instance)
            {
                var existingInstance = FindObjectOfType<Loader>();

                if (existingInstance != null)
                {
                    _instance = existingInstance;
                    DontDestroyOnLoad(_instance.gameObject);
                }
            }
        }

        private void OnDestroy()
        {
            if (_isSingleton)
                _instance = null;
        }

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------
        public void Visiable(float duration, Action loadWork, Action doneCallBack)
        {
            if (_co_visiable == null)
                _co_visiable = StartCoroutine(_Co_Visiable(duration, loadWork, doneCallBack));
        }

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------
        private IEnumerator _Co_Visiable(float duration, Action loadWork, Action doneCallBack)
        {
            _animation.clip = _animation.GetClip(SHOW_TRIGGER);
            _animation.Play();

            var showSec = _animation.clip.length;
            yield return new WaitForSeconds(showSec);

            loadWork?.Invoke();
            _animation.clip = _animation.GetClip(IDLE_TRIGGER);
            _animation.Play();

            yield return new WaitForSeconds(duration);

            _animation.clip = _animation.GetClip(HIDE_TRIGGER);
            _animation.Play();

            var hideSec = _animation.clip.length;
            yield return new WaitForSeconds(hideSec);

            doneCallBack?.Invoke();
            _co_visiable = null;
        }
    }
}