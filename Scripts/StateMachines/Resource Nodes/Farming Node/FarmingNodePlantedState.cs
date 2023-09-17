using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.FarmingNode
{
    public class FarmingNodePlantedState : FarmingNodeBaseState
    {
        private float timeInState;

        public FarmingNodePlantedState(FarmingNodeStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.PlantedState.SetActive(true);
            timeInState = stateMachine.TimeBetweenStates;
        }

        public override void Tick(float deltaTime)
        {
            timeInState -= deltaTime;

            if(timeInState <= 0)
            {
                stateMachine.SwitchState(new FarmingNodeFirstGrowthState(stateMachine));
                return;
            }
        }

        public override void Exit()
        {
            stateMachine.PlantedState.SetActive(false);
        }
    }
}