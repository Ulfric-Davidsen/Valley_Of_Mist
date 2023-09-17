using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.AI
{
    public class EnemyChasingState : EnemyBaseState
    {
        private readonly int LocomotionSpeedHash = Animator.StringToHash("LocomotionSpeed");
        private readonly int LocomotionBlendTreeHash = Animator.StringToHash("LocomotionBlendTree");

        private const float AnimatorDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;

        public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

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
            if(!IsInChaseRange())
            {
                Debug.Log("Not In Range");
                stateMachine.SwitchState(new EnemyIdleState(stateMachine));
                return;
            }
            else if(IsInAttackRange())
            {
                stateMachine.SwitchState(new EnemyAttackingState(stateMachine, 0));
                return;
            }

            MoveToPlayer(deltaTime);

            FacePlayer();

            stateMachine.Animator.SetFloat(LocomotionSpeedHash, 1, AnimatorDampTime, deltaTime);
        }

        public override void Exit()
        {
            stateMachine.NavMeshAgent.ResetPath();
            stateMachine.NavMeshAgent.velocity = Vector3.zero;
        }

        private void MoveToPlayer(float deltaTime)
        {
            if(stateMachine.NavMeshAgent.isOnNavMesh)
            {
                stateMachine.NavMeshAgent.destination = stateMachine.Player.transform.position;

                Move(stateMachine.NavMeshAgent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
            }
            
            stateMachine.NavMeshAgent.velocity = stateMachine.Controller.velocity;
        }

        private bool IsInAttackRange()
        {
            if(stateMachine.Player.IsDead) { return false; }
            
            float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

            return playerDistanceSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
        }
    }
}