// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

namespace Utility.ForObjectPool
{
    [System.Serializable]
    public sealed class ObjectPool<TKey> : MonoBehaviour where TKey : Component
    {
        // --------------------------------------------------
        // Constructor
        // --------------------------------------------------
        public ObjectPool(int capacity) { this._capacity = capacity; }

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        private TKey        _targetObject = default;
        private Queue<TKey> _pool         = default;
        private Transform   _parents      = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private int _capacity = 0;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public TKey OriginObject => _targetObject;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void OnInit(TKey targetObject, Transform parents)
        {
            if (_targetObject != null)
                return;

            _targetObject = targetObject;
            _parents = parents;

            _pool = new Queue<TKey>();

            for (int i = 0; i < _capacity; i++) _pool.Enqueue(_CreatedObejct());
        }

        public TKey GetObject(Transform parents = null)
        {
            TKey obj = default;

            if (_pool.Count > 0)
            {
                obj = _pool.Dequeue();
            }
            else
            {
                obj = _CreatedObejct();
                _pool.Enqueue(obj);
            }

            obj.transform.SetParent(parents);
            obj.gameObject.SetActive(true);

            return obj;
        }

        public void ReturnObject(TKey obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_parents);
            _pool.Enqueue(obj);
        }

        // ----- Private
        private TKey _CreatedObejct()
        {
            TKey obj = Instantiate(_targetObject, _parents).GetComponent<TKey>();
            obj.gameObject.SetActive(false);
            return obj;
        }

    }
}