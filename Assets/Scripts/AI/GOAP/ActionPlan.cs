using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI.GOAP
{
    internal class ActionPlan
    {
        public AgentGoal Goal { get; }
        public Stack<AgentAction> Actions { get; }
        public float TotalCost { get; set; }

        public ActionPlan(AgentGoal goal, Stack<AgentAction> actions, float totalCost)
        {
            Goal = goal;
            Actions = actions;
            TotalCost = totalCost;
        }


    }
}
