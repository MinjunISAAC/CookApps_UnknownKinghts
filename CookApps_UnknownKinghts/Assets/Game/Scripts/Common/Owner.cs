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
            // [TODO] �⺻ ���ÿ� �ʿ��� ���� ����

            // Currency System �ʱ�ȭ
            CurrencySystem.Instance.OnInit();

            // Loader �����ֱ�

            // State �ʱ�ȭ
            StateMachine.Instance.ChangeState(EStateType.Village);
           
            yield return null;
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Private
        
    }
}