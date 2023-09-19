using InGame.ForUnit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForState.ForBattle
{
    public class UnitHp : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private RectTransform _RECT_HpBar = null;
        [SerializeField] private RectTransform _RECT_Gauge = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Unit    _targetUnit = null;
        private Vector2 _gaugeSize  = Vector2.zero;
        private float   _maxWidth   = 0.0f;
        private float   _maxHealth  = 0.0f;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Start() 
        { 
            _gaugeSize = _RECT_Gauge.rect.size;
            _maxWidth  = _gaugeSize.x;
        }

        private void Update() 
        {
            if (_targetUnit == null)
                return;

            var pos = Camera.main.WorldToScreenPoint(_targetUnit.transform.position);

            _RECT_HpBar.transform.position = pos;

            _gaugeSize.x = (_maxWidth * _targetUnit.Health) / (float)_maxHealth;

            _RECT_Gauge.sizeDelta = _gaugeSize;

            if (_targetUnit.UnitState == Unit.EState.Die)
                Destroy(gameObject);
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void Create(Unit targetUnit) 
        {
            _targetUnit = targetUnit;
            _maxHealth  = _targetUnit.UnitData.Ability.Health;

            var pos = Camera.main.WorldToScreenPoint(_targetUnit.transform.position);
            _RECT_HpBar.transform.position = pos;
        } 
    }
}