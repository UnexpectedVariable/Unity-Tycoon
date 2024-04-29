using Assets.Scripts.Building.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Assets.Scripts.Building
{
    internal class BuildingManager : MonoBehaviour
    {
        [SerializeField]
        private BuildingButton _button = null;

        [SerializeField]
        private InputAction _press = null;
        [SerializeField]
        private InputAction _position = null;

        [SerializeField]
        private GameObject _building = null;
        [SerializeField]
        private BuildingSpot _buildingSpot = null;

        private Camera _mainCamera = null;

        private Vector3 _airbornScale = new Vector3(0.3f, 0.3f, 0.3f);

        private void Awake()
        {
            InitializeCamera();
            InitializeBuildButtons();
            InitializeInputActions();
        }

        private void InitializeCamera()
        {
            _mainCamera = Camera.main;
        }

        private void InitializeBuildButtons()
        {
            _button.OnPressedEvent += (sender, args) =>
            {
                _position.Enable();
            };
            _button.OnPrefabInstanstiatedEvent += (sender, args) =>
            {
                _building = Instantiate(args, _button.transform.position, Quaternion.identity);
                _building.transform.localScale = _airbornScale;
            };
        }

        private void InitializeInputActions()
        {
            _press.Enable();
            _press.canceled += (context) =>
            {
                Debug.Log("Mouse press canceled");
                if (!_position.enabled) return;
                _position.Disable();
                FinalizeBuilding();
                Clear();
            };

            _position.performed += (context) =>
            {
                Debug.Log("Mouse position changed");
                Vector3 mousePos = _position.ReadValue<Vector2>();
                Vector3 finalPos = mousePos;
                SnapBuildingToCursor(mousePos, ref finalPos);
                SnapBuildingToSpot(mousePos, ref finalPos);
                Debug.Log($"Setting building final pos to {finalPos}");
                _building.transform.position = finalPos;
            };
        }

        private void FinalizeBuilding()
        {
            if (_buildingSpot != null && !_buildingSpot.Occupied)
            {
                Debug.Log($"{_building} built!");
                _buildingSpot.Occupy(_building);
            }
            else
            {
                Debug.Log($"{_building} discarded!");
                Destroy(_building);
            }
        }

        private void SnapBuildingToSpot(Vector3 mousePos, ref Vector3 buildingPos)
        {
            var ray = _mainCamera.ScreenPointToRay(mousePos);
            if (!Physics.Raycast(ray, out var hit)) return;

            Debug.Log($"Hit {hit.collider.name}");
            if (!Data.TryMatch(hit.collider.tag, out _))
            {
                Debug.Log($"Building spot not found: {_buildingSpot == null}");
                if (_buildingSpot == null) return;
                _building.transform.localScale = _airbornScale;
                _buildingSpot = null;
                return;
            }
            Debug.Log($"Building spot found, setting prefab pos to {hit.transform.position}");
            buildingPos = hit.transform.position;
            if (_buildingSpot != null && _buildingSpot.gameObject == hit.collider.gameObject) return;
            _building.transform.localScale = Vector3.one;
            _buildingSpot = hit.collider.gameObject.GetComponent<BuildingSpot>();
            return;
        }

        private void SnapBuildingToCursor(Vector3 mousePos, ref Vector3 buildingPos)
        {
            mousePos.z = 10f;
            var position = _mainCamera.ScreenToWorldPoint(mousePos);
            buildingPos = position;
        }

        private void Clear()
        {
            _building = null;
            _buildingSpot = null;
        }
    }
}
