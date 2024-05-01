using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Building
{
    internal class BuildingButton : MonoBehaviour, IPointerDownHandler
    {
        public event EventHandler<uint> OnPressedEvent = null;
        public event EventHandler<GameObject> OnPrefabInstanstiatedEvent = null;

        [SerializeField]
        private Building _building = null;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPressedEvent?.Invoke(this, _building.Cost);
            OnPrefabInstanstiatedEvent?.Invoke(this, _building.Prefab);
        }
    }
}
