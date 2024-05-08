using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP
{
    internal class Planner
    {
        public ActionPlan Plan(Agent agent, HashSet<AgentGoal> goals, AgentGoal lastGoal = null)
        {
            List<AgentGoal> orderedGoals = goals
                .Where(goal => goal.Goals.Any(belief => !belief.Evaluate()))
                .OrderByDescending(goal => goal == lastGoal ? goal.Priority - float.MinValue : goal.Priority)
                .ToList();

            foreach (var goal in orderedGoals)
            {
                Node root = new(null, null, goal.Goals, 0);

                if (FindPath(root, agent.Actions))
                {
                    if (root.IsLeafDead) continue;

                    var actionStack = new Stack<AgentAction>();
                    while (root.Leaves.Count > 0)
                    {
                        root = root.Leaves.OrderBy(leaf => leaf.Cost).First();
                        actionStack.Push(root.Action);
                    }

                    return new ActionPlan(goal, actionStack, root.Cost);
                }
            }

            Debug.LogWarning($"No plan found");
            return null;
        }

        private bool FindPath(Node parent, HashSet<AgentAction> actions)
        {
            foreach (var action in actions)
            {
                var requirements = parent.Requirements;

                requirements.RemoveWhere(belief => belief.Evaluate());

                if (requirements.Count == 0) return true;

                if(action.Effects.Any(requirements.Contains))
                {
                    var newRequirements = new HashSet<AgentBelief>(requirements);
                    newRequirements.ExceptWith(action.Effects);
                    newRequirements.UnionWith(action.Preconditions);

                    var availableActions = new HashSet<AgentAction>(actions);
                    availableActions.Remove(action);

                    var leave = new Node(parent, action, newRequirements, parent.Cost + action.Cost);

                    if(FindPath(leave, availableActions))
                    {
                        parent.Leaves.Add(leave);
                        newRequirements.ExceptWith(leave.Action.Preconditions);
                    }

                    if(newRequirements.Count == 0) return true;
                }
            }

            return false;
        }
    }

    internal class Node
    {
        public Node parent { get; }
        public AgentAction Action { get; }
        public HashSet<AgentBelief> Requirements { get; }
        public List<Node> Leaves { get; }
        public float Cost { get; }

        public bool IsLeafDead => Leaves.Count < 0 && Action == null;

        public Node(Node parent, AgentAction action, HashSet<AgentBelief> requirements, float cost)
        {
            this.parent = parent;
            Action = action;
            Requirements = new(requirements);
            Leaves = new();
            Cost = cost;
        }
    }
}
