using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Util
{
    internal interface ISelectable
    {
        void OnSelected();
        void OnDeselected();
    }
}
