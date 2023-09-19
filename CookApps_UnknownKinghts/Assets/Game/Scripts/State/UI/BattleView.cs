// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForBattle.ForUI;
using System;
using UnityEngine.UI;
using TMPro;
using InGame.ForState.ForBattle;
using InGame.ForUnit;

namespace InGame.ForState.ForUI
{
    public class BattleView : StateView
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private Image            _chapterInfo = null;
        [SerializeField] private TextMeshProUGUI _chapterText = null;

        [Space]
        [SerializeField] private TimerView        _timerView   = null;
        [SerializeField] private BattleBottomView _bottomView  = null;

        [Space]
        [SerializeField] private UnitHp           _originUnitUp  = null;
        [SerializeField] private UnitHit          _originUnitHit = null;
        [SerializeField] private Transform        _unitPerants   = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public override void OnInit()   { }
        public override void OnFinish() { }

        public void SetToBottomView(Action onClickTimeScale)
        {
            _bottomView.SetToTimeScaleBtn(onClickTimeScale);
        }
        public void VisiableToTimeScaleBtn(bool visiable) => _bottomView.VisiableTimeScaleBtn(visiable);

        public void SetToTimer(float duration) => _timerView.SetToTimer(duration);
        public void PlayToTimer(float duration, Action doneCallBack) => _timerView.PlayToTimer(duration, doneCallBack);

        public void CreatedToHpBar(Unit unit) 
        {
            var hpBar = Instantiate(_originUnitUp, _unitPerants);
            hpBar.Create(unit);
        } 

        public UnitHit ShowToHitInfo(int power)
        {
            var hit = Instantiate(_originUnitHit, _unitPerants);
            hit.Show(power);
            return hit;
        }

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------
        public IEnumerator BattleStartView(float duration, Action doneCallBack)
        {
            _chapterInfo.gameObject.SetActive(true);

            yield return new WaitForSeconds(duration);
            
            _chapterInfo.gameObject.SetActive(false);

            doneCallBack?.Invoke();
        }
    }
}