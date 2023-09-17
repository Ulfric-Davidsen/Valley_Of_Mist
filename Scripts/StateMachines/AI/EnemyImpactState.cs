using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.AI
{
    public class EnemyImpactState : EnemyBaseState
    {
        private readonly int ImpactHash = Animator.StringToHash("Impact");

        private const float CrossFadeDuration = 0.1f;

        private float duration = 0.75f;

        public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            if(stateMachine.NavMeshAgent.enabled == true)
            {
                stateMachine.NavMeshAgent.enabled = false;
            }
            
            stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);

            duration -= deltaTime;

            if(duration <= 0f)
            {
                stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            }
        }

        public override void Exit()
        {

        }
    }
}