using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using InGame.ForUnit;

namespace InGame.ForState.ForBuildDeck
{
    public class BuildDeckArea : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        public enum ECountType
        {
            Unknown = 0,
            One     = 1,
            Two     = 2,
            Three   = 3,
        }

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private EAttackPosType  _attackPosType = EAttackPosType.Unknown;
        
        [SerializeField] private List<Transform> _oneList       = new List<Transform>();
        [SerializeField] private List<Transform> _twoList       = new List<Transform>();
        [SerializeField] private List<Transform> _threeList     = new List<Transform>();
        
        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public EAttackPosType AttackPosType => _attackPosType;
        public int            MaxCopacity   => _threeList.Count;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Dictionary<ECountType, List<Transform>> _positionSet = new Dictionary<ECountType, List<Transform>>();

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Start()
        {
            _positionSet.Add(ECountType.One,   _oneList  );
            _positionSet.Add(ECountType.Two,   _twoList  );
            _positionSet.Add(ECountType.Three, _threeList);
        }

        private void OnDestroy()
        {
            _positionSet.Clear();
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public List<Transform> GetToAreaList(int count)
        {
            if      (count == 1) return _oneList;
            else if (count == 2) return _twoList;
            else                 return _threeList;
        }
    }
}