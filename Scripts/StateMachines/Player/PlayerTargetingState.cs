using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
        private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
        private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

        private const float CrossFadeDuration = 0.1f;

        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.InputReader.TargetEvent += OnTarget;
            stateMachine.InputReader.DodgeEvent += OnDodge;
            stateMachine.InputReader.JumpEvent += OnJump;

            stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            if(stateMachine.InputReader.IsAttacking)
            {
                stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
                return;
            }

            if(stateMachine.InputReader.IsBlocking)
            {
                stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
                return;
            }

            if(stateMachine.Targeter.CurrentTarget == null)
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                return;
            }

            Vector3 movement = CalculateTargetingMovement(deltaTime);
            
            Move(movement *  stateMachine.TargetingMovementSpeed, deltaTime);

            UpdateAnimator(deltaTime);

            FaceTarget();
        }

        public override void Exit()
        {
            stateMachine.InputReader.TargetEvent -= OnTarget;
            stateMachine.InputReader.DodgeEvent -= OnDodge;
            stateMachine.InputReader.JumpEvent -= OnJump;
        }

        private void OnTarget()
        {
            stateMachine.Targeter.CancelTarget();

            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

        private void OnDodge()
        {
            if(stateMachine.InputReader.MovementValue == Vector2.zero) { return; }
            stateMachine.SwitchState(new PlayerDodgingState(stateMachine, stateMachine.InputReader.MovementValue));
        }

        private void OnJump()
        {
            stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
        }

        private void UpdateAnimator(float deltaTime)
        {
            if (stateMachine.InputReader.MovementValue.y == 0)
            {
                stateMachine.Animator.SetFloat(TargetingForwardHash, 0, 0.1f, deltaTime);
            }
            else
            {
                float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
                stateMachine.Animator.SetFloat(TargetingForwardHash, value, 0.1f, deltaTime);
            }

            if (stateMachine.InputReader.MovementValue.x == 0)
            {
                stateMachine.Animator.SetFloat(TargetingRightHash, 0, 0.1f, deltaTime);
            }
            else
            {
                float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
                stateMachine.Animator.SetFloat(TargetingRightHash, value, 0.1f, deltaTime);
            }
        }
    }
}
