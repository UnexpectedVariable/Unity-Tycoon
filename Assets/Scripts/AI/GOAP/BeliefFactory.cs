using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using static Assets.Scripts.AI.GOAP.AgentBelief;

namespace Assets.Scripts.AI.GOAP
{
    internal class BeliefFactory
    {
        //private readonly GoapAgent _agent;
        private readonly Dictionary<string, AgentBelief> _beliefs;
        private readonly Agent _agent;

        public BeliefFactory(Agent agent, Dictionary<string, AgentBelief> beliefs)
        {
            _beliefs = beliefs;
            _agent = agent;
        }

        public void AddBelief(string key, ConditionDelegate condition)
        {
            _beliefs.Add(key, new AgentBelief.Builder(key)
                .WithCondition(condition)
                .Build());
        }
    }
}
