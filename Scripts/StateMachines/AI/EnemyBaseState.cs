using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.AI
{
    public abstract class EnemyBaseState : State
    {
        protected EnemyStateMachine stateMachine;

        public EnemyBaseState(EnemyStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        protected void Move(float deltaTime)
        {
            Move(Vector3.zero, deltaTime);
        }

        protected void Move(Vector3 motion, float deltaTime)
        {
            stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
        }

        protected void FacePlayer()
        {
            if(stateMachine.Player == null) { return; }

            Vector3 lookPosition = stateMachine.Player.transform.position - stateMachine.transform.position;
            lookPosition.y = 0f;

            stateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
        }

        protected bool IsInChaseRange()
        {
            if(stateMachine.Player.IsDead) { return false; }
            
            float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

            return playerDistanceSqr <= stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange;
        }
    }
}