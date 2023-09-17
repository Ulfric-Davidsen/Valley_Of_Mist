using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.FarmingNode
{
    public class FarmingNodeHarvestState : FarmingNodeBaseState
    {
        public FarmingNodeHarvestState(FarmingNodeStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.HarvestState.SetActive(true);
        }

        public override void Tick(float deltaTime)
        {
            
        }

        public override void Exit()
        {
            stateMachine.HarvestState.SetActive(false);
        }
    }
}