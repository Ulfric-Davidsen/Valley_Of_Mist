using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.FarmingNode
{
    public class FarmingNodeSecondGrowthState : FarmingNodeBaseState
    {
        private float timeInState;

        public FarmingNodeSecondGrowthState(FarmingNodeStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.SecondGrowthState.SetActive(true);
            timeInState = stateMachine.TimeBetweenStates;
        }

        public override void Tick(float deltaTime)
        {
            timeInState -= deltaTime;

            if(timeInState <= 0)
            {
                stateMachine.SwitchState(new FarmingNodeHarvestState(stateMachine));
                return;
            }
        }

        public override void Exit()
        {
            stateMachine.SecondGrowthState.SetActive(false);
        }
    }
}