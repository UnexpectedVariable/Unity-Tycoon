using Assets.Scripts.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    internal class MouseManager : MonoBehaviour
    {
        [SerializeField]
        private InputAction _press = null;

        private Camera _mainCamera = null;

        private ISelectable _currentSelection = null;

        private void Start()
        {
            _mainCamera = Camera.main;

            _press.Enable();
            _press.started += (context) =>
            {
                var mousePos = context.ReadValue<Vector2>();
                var ray = _mainCamera.ScreenPointToRay(mousePos);
                if (!Physics.Raycast(ray, out var hit)) return;

                if (_currentSelection != null) _currentSelection.OnDeselected();
                _currentSelection = hit.collider.gameObject.GetComponent<ISelectable>();
                if (_currentSelection == null) return;
                _currentSelection.OnSelected();
            };
        }
    }
}
