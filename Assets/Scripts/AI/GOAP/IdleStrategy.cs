using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI.GOAP
{
    internal class IdleStrategy : IActionStrategy
    {
        public bool CanPerform => true;

        public bool IsComplete { get; private set; }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }
    }
}
