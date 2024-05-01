using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Resources
{
    internal class ResourceManager : MonoBehaviour
    {
        [SerializeField]
        private ResourcesHolder _holder = null;

        public uint ChangeAmount = 0;

        [ContextMenu("Add Gold")]
        public void AddGold()
        {
            _holder.AddResource(Util.Data.Resources.Gold, ChangeAmount);
        }
    }
}
