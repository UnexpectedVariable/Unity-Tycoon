using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Building
{
    internal class BuildingSpotUI : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas = null;

        [SerializeField]
        private Button _destroyButton = null;

        public event EventHandler OnDestroyEvent = null;

        private void Start()
        {
            if (_canvas == null) _canvas = GetComponentInChildren<Canvas>(true);
            //if (_raycaster == null) _raycaster = GetComponentInChildren<GraphicRaycaster>(true);

            if(_destroyButton == null) _destroyButton = GetComponentInChildren<Button>(true);
            _destroyButton.onClick.AddListener(() =>
            {
                Debug.Log($"Destroy button on {name} clicked!");
                OnDestroyEvent?.Invoke(this, EventArgs.Empty);
            });
        }

        public void SetCanvasActive(bool value)
        {
            Debug.Log($"Setting {name} canvas coponent to {value}");
            _canvas.gameObject.SetActive(value);
        }
    }
}
