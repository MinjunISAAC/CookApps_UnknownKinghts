// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.SimpleFSM;

namespace InGame.ForState
{
    public class StateMachine : SimpleStateMachine<EStateType>
    {
        // --------------------------------------------------
        // Singleton
        // --------------------------------------------------
        // ----- Constructor
        private StateMachine() { }

        // ----- Static Variables
        private static StateMachine _instance = null;

        // ----- Property
        public static StateMachine Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new StateMachine();
                    _instance._InitSingleton();
                }

                return _instance;
            }
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        private class CoroutineExecoutor : MonoBehaviour { }
        private void _InitSingleton()
        {
            if (null == _coroutineExecutor)
            {
                GameObject executorGameObject = new GameObject("CoroutineExecutor");

                _coroutineExecutor = executorGameObject.AddComponent<CoroutineExecoutor>();
                if (null == _coroutineExecutor)
                {
                    Debug.LogError("[StateMachine._InitSingleton] Coroutine 실행자가 생성되지 않았습니다.");
                    return;
                }
                UnityEngine.Object.DontDestroyOnLoad(executorGameObject);
            }
            /*
            OnInit
            (
                new Dictionary<EStateType, SimpleState<EStateType>>()
                {
                    { EStateType.Ready, new State_Ready() },
                    { EStateType.Play,  new State_Play()  },
                    { EStateType.End,   new State_End()   },
                },
                _coroutineExecutor,
                null
            );
            */
        }
    }
}