using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI.GOAP
{
    internal class AgentBelief
    {
        public string Name { get; }

        public delegate bool ConditionDelegate();
        private ConditionDelegate _condition = () => false;

        public AgentBelief(string name)
        {
            Name = name;
        }

        public bool Evaluate() => _condition();

        public class Builder
        {
            readonly AgentBelief _belief;

            public Builder(string name)
            {
                _belief = new AgentBelief(name);
            }

            public Builder WithCondition(ConditionDelegate condition)
            {
                _belief._condition = condition;
                return this;
            }

            public AgentBelief Build()
            {
                return _belief;
            }
        }
    }
}
