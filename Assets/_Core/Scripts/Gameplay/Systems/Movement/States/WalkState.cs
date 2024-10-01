using System;
using Odumbrata.Systems.Movement.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Systems.Movement.States
{
    [Serializable]
    public class WalkState : BaseMoveState<WalkData>
    {
        private NavMeshAgent Agent => Data.Agent;

        public override void OnEntered()
        {
            Debug.Log("Walk entered");

            Agent.isStopped = false;

            Agent.speed = Data.WalkStat.Value;

            Agent.SetPath(Data.Path);
        }

        public override void OnExited()
        {
            Agent.isStopped = true;
            Debug.Log("Walk exited");
        }
    }
}