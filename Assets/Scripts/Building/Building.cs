using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Building
{
    [CreateAssetMenu(fileName = "Building", menuName = "Buildings/Building", order = 0)]
    internal class Building : ScriptableObject
    {
        [SerializeField]
        private GameObject _prefab = null;
        [SerializeField]
        private uint _cost = 0;

        public GameObject Prefab => _prefab;
        public uint Cost => _cost;
    }
}
