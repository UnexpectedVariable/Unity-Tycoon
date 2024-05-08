using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI.GOAP
{
    [RequireComponent(typeof(NavMeshAgent))]
    internal class Agent : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent = null;

        [Header("Stats")]
        public float Patience = 100f;

        private GameObject _target = null;
        private Vector3 _destination = Vector3.zero;

        private AgentGoal _lastGoal = null;
        public AgentGoal CurrentGoal = null;
        public ActionPlan ActionPlan = null;
        public AgentAction CurrentAction = null;

        public Dictionary<string, AgentBelief> BeliefMap = new();
        public HashSet<AgentAction> Actions = new();
        public HashSet<AgentGoal> Goals = new();

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            InitializeBeliefs();
            InitializeActions();
            InitializeGoals();
        }

        private void InitializeBeliefs()
        {
            BeliefFactory factory = new BeliefFactory(this, BeliefMap);

            factory.AddBelief("Nothing", () => false);

            factory.AddBelief("Stationary", () => !_navMeshAgent.hasPath);
            factory.AddBelief("Moving", () => _navMeshAgent.hasPath);
        }

        private void InitializeActions()
        {
            Actions.Add(new AgentAction.Builder("Idle")
                .WithStrategy(new IdleStrategy(5))
                .AddEffects(BeliefMap["Nothing"])
                .Build());
        }

        private void InitializeGoals()
        {
            Goals.Add(new AgentGoal.Builder("Relax")
                .WithPriority(1)
                .AddGoals(BeliefMap["Nothing"])
                .Build());
        }
    }
}
