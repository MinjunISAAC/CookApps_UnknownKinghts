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
        private const string HIDE_TRIGGER = "Loader_Hide";

        // ----- Private
        private Coroutine _co_Visiable = null;

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
        public void Show(Action loadWork, Action doneCallBack, float duration = 0)
        {
            if (_co_Visiable == null)
                _co_Visiable = StartCoroutine(_Co_Show(duration, loadWork, doneCallBack));
        }

        public void Hide(Action loadWork, Action doneCallBack)
        {
            if (_co_Visiable == null)
                _co_Visiable = StartCoroutine(_Co_Hide(loadWork, doneCallBack));
        }

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------
        private IEnumerator _Co_Show(float duration, Action loadWork, Action doneCallBack)
        {
            _animation.clip = _animation.GetClip(SHOW_TRIGGER);
            _animation.Play();

            loadWork?.Invoke();

            var delay = _animation.clip.length;
            
            if (delay < duration) yield return new WaitForSeconds(duration);
            else                  yield return new WaitForSeconds(delay);

            _co_Visiable = null;
            doneCallBack?.Invoke();
        }

        private IEnumerator _Co_Hide(Action loadWork, Action doneCallBack)
        {
            _animation.clip = _animation.GetClip(HIDE_TRIGGER);
            _animation.Play();

            loadWork?.Invoke();

            var delay = _animation.clip.length;
            yield return new WaitForSeconds(delay);

            _co_Visiable = null;
            doneCallBack?.Invoke();
        }
    }
}