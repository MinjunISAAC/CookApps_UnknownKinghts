// ----- C#
using System.Collections;

// ----- Unity
using UnityEngine;

namespace Utility.SimpleFSM
{
    public abstract class SimpleState<EState>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private System.Action<EState, object> _changeStateCallBack = null;
        private MonoBehaviour                 _coroutineExecutor   = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public abstract EState State { get; }

        // --------------------------------------------------
        // Functions - Public Using
        // --------------------------------------------------
        public void Init(System.Action<EState, object> changeStateCallBack,
                         MonoBehaviour                 coroutineExecutor,
                         object                        initParam = null)
        {
            Release();

            _changeStateCallBack = changeStateCallBack;
            _coroutineExecutor   = coroutineExecutor;

            _Init(initParam);
        }

        public void Release()
        {
            _changeStateCallBack = null;
            _coroutineExecutor   = null;

            _Release();
        }

        public void Start (EState preStateType, object startParam = null) { _Start(preStateType, startParam); }
        public void Finish(EState nextStateType)                          { _Finish(nextStateType); }
        public void Update()                                              { _Update(); }

        // --------------------------------------------------
        // Functions - Protected Virtual (Required Implementer)
        // --------------------------------------------------
        protected virtual void _Init   (object initParam)                      { }
        protected virtual void _Release()                                      { }
        protected virtual void _Start  (EState preStateKey, object startParam) { }
        protected virtual void _Finish (EState nextStateKey)                   { }
        protected virtual void _Update ()                                      { }

        // --------------------------------------------------
        // Functions - State
        // --------------------------------------------------
        protected void ChangeState(EState nextStateType, object param = null)
        {
            _changeStateCallBack?.Invoke(nextStateType, param);
        }

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------
        protected Coroutine StartCoroutine(IEnumerator enumerator)
        {
            if (null == _coroutineExecutor)
            {
                Debug.LogError($"[SimpleState.StartCoroutine] 코루틴 실행자가 존재하지 않습니다.");
                return null;
            }

            return _coroutineExecutor.StartCoroutine(enumerator);
        }

        protected void StopCoroutine(Coroutine coroutine)
        {
            if (null == coroutine || null == _coroutineExecutor)
                return;

            _coroutineExecutor.StopCoroutine(coroutine);
        }
    }
}