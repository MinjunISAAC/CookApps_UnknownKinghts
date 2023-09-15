// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUI;
using InGame.ForState;
using Utility.ForCurrency;

namespace InGame
{
    public class Owner : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("0. UI Group")]
        [SerializeField] private UIOwner _mainUI = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public static Owner NullableInstance
        {
            get;
            private set;
        } = null;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Awake() { NullableInstance = this; }

        private IEnumerator Start()
        {
            // [TODO] 기본 셋팅에 필요한 내용 적용

            // Currency System 초기화
            CurrencySystem.Instance.OnInit();

            // Loader 보여주기

            // State 초기화
            StateMachine.Instance.ChangeState(EStateType.Village);
           
            yield return null;
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Private
        
    }
}