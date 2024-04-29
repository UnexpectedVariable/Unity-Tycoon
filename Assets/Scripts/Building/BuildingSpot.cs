using Assets.Scripts.Building.Util;
using Assets.Scripts.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Building
{
    internal class BuildingSpot : MonoBehaviour, ISelectable
    {
        private readonly Data.Tags _defaultTag = Data.Tags.BuildingSpot;

        private GameObject _building = null;

        [SerializeField]
        private Canvas _canvas = null;

        private bool _occupied = false;
        public bool Occupied => _occupied;

        public event EventHandler OnBuiltUpEvent = null;
        public event EventHandler OnSelectedEvent = null;
        public event EventHandler OnDeselectedEvent = null;

        private void Start()
        {
            gameObject.tag = Data.TagToString(_defaultTag);

            if(_canvas == null) _canvas = GetComponentInChildren<Canvas>(true);

            OnBuiltUpEvent += (sender, args) =>
            {
                _occupied = true;
            };
            OnSelectedEvent += (sender, args) =>
            {
                _canvas.gameObject.SetActive(true);
            };
            OnDeselectedEvent += (sender, args) =>
            {
                _canvas.gameObject.SetActive(false);
            };
        }

        public void Occupy(GameObject building)
        {
            _building = building;
            OnBuiltUpEvent?.Invoke(this, EventArgs.Empty);
        }

        public void OnSelected()
        {
            OnSelectedEvent?.Invoke(this, EventArgs.Empty);
        }

        public void OnDeselected()
        {
            OnDeselectedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
