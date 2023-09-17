using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.AI
{
    public class EnemyIdleState : EnemyBaseState
    {
        private readonly int LocomotionSpeedHash = Animator.StringToHash("LocomotionSpeed");
        private readonly int LocomotionBlendTreeHash = Animator.StringToHash("LocomotionBlendTree");

        private const float AnimatorDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;

        public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            if(stateMachine.NavMeshAgent.enabled == false)
            {
                stateMachine.NavMeshAgent.enabled = true;
            }

            stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);

            if(IsInChaseRange())
            {
                Debug.Log("In Range");
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
                return;
            }

            stateMachine.Animator.SetFloat(LocomotionSpeedHash, 0, AnimatorDampTime, deltaTime);
        }

        public override void Exit()
        {
            
        }
    }
}