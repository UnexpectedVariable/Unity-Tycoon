using Assets.Scripts.Resources.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Resources
{
    internal class ResourcesHolder : MonoBehaviour
    {
        [SerializeField]
        private List<ResourceHolder> _resources = new();
        private Dictionary<Data.Resources, ResourceHolder> _resourceMap = new();

        private void Start()
        {
            if(_resources.Count == 0) _resources = GetComponentsInChildren<ResourceHolder>().ToList();

            foreach (var resource in _resources)
            {
                _resourceMap.Add(resource.Type, resource);
            }
        }

        public bool TryGetResource(Data.Resources type, uint amount)
        {
            var available = _resourceMap[type].Amount;
            if(available < amount) return false;

            _resourceMap[type].SetAmount(available - amount);
            return true;
        }

        public void AddResource(Data.Resources type, uint amount)
        {
            var available = _resourceMap[type].Amount;
            _resourceMap[type].SetAmount(available + amount);
        }
    }
}
