using Assets.Scripts.Resources.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Resources
{
    internal class ResourceHolder : MonoBehaviour
    {
        [SerializeField]
        private Data.Resources _type = default;

        [SerializeField]
        private TextMeshProUGUI _amountTMP;

        public uint Amount => uint.Parse(_amountTMP.text);
        public Data.Resources Type => _type;

        private void Start()
        {
            if(_amountTMP == null) _amountTMP = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetAmount(uint amount)
        {
            _amountTMP.text = $"{amount}";
        }
    }
}
