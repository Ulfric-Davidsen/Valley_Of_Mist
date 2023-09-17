using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.FarmingNode
{
    public class FarmingNodeFirstGrowthState : FarmingNodeBaseState
    {
        private float timeInState;

        public FarmingNodeFirstGrowthState(FarmingNodeStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.FirstGrowthState.SetActive(true);
            timeInState = stateMachine.TimeBetweenStates;
        }

        public override void Tick(float deltaTime)
        {
            timeInState -= deltaTime;

            if(timeInState <= 0)
            {
                stateMachine.SwitchState(new FarmingNodeSecondGrowthState(stateMachine));
                return;
            }
        }

        public override void Exit()
        {
            stateMachine.FirstGrowthState.SetActive(false);
        }
    }
}