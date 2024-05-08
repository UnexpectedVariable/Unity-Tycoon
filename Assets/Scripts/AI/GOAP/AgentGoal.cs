using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI.GOAP
{
    internal class AgentGoal
    {
        public string Name { get; }
        public float Priority { get; private set; }
        public HashSet<AgentBelief> Goals { get; } = new();

        private AgentGoal(string name)
        {
            Name = name;
        }

        public class Builder
        {
            private readonly AgentGoal _goal;

            public Builder(string name)
            {
                _goal = new AgentGoal(name);
            }

            public Builder WithPriority(float priority)
            {
                _goal.Priority = priority;
                return this;
            }

            public Builder AddGoals(AgentBelief belief)
            {
                _goal.Goals.Add(belief);
                return this;
            }

            public AgentGoal Build()
            {
                return _goal;
            }
        }
    }
}
