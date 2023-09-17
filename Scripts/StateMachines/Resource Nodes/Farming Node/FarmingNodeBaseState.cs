using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.FarmingNode
{
    public abstract class FarmingNodeBaseState : State
    {
        protected FarmingNodeStateMachine stateMachine;

        public FarmingNodeBaseState(FarmingNodeStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}