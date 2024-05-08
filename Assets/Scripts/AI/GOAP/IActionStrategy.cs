using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI.GOAP
{
    internal interface IActionStrategy
    {
        bool CanPerform { get; }
        bool IsComplete { get; }

        void Start();
        void Update(float deltaTime);
        void Stop();
    }
}
