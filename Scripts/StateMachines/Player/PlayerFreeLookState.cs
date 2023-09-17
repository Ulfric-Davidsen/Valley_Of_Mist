using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.Player
{
    public class PlayerFreeLookState : PlayerBaseState
    {
        private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
        private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");

        private const float AnimatorDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;

        private bool shouldFade;

        public PlayerFreeLookState(PlayerStateMachine stateMachine, bool shouldFade = true) : base(stateMachine)
        {
            this.shouldFade = shouldFade;
        }

        public override void Enter()
        {
            stateMachine.InputReader.SprintEvent += OnSprint;
            stateMachine.InputReader.JumpEvent += OnJump;
            stateMachine.InputReader.TargetEvent += OnTarget;
            stateMachine.InputReader.InteractEvent += OnInteract;

            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f);

            if(shouldFade)
            {
                stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);
            }
            else
            {
                stateMachine.Animator.Play(FreeLookBlendTreeHash);
            }
            
        }

        public override void Tick(float deltaTime)
        {
            if(stateMachine.InputReader.IsAttacking && !IsMouseOverUI())
            {
                stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
                return;
            }

            Vector3 movement = CalculateFreeLookMovementDirection();

            Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

            if(stateMachine.InputReader.MovementValue == Vector2.zero)
            {
                stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f, AnimatorDampTime, deltaTime);
                return;
            }
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1f, AnimatorDampTime, deltaTime);

            FaceMovementDirection(movement, deltaTime);
        }

        public override void Exit()
        {
            stateMachine.InputReader.SprintEvent -= OnSprint;
            stateMachine.InputReader.JumpEvent -= OnJump;
            stateMachine.InputReader.TargetEvent -= OnTarget;
            stateMachine.InputReader.InteractEvent -= OnInteract;
        }

        private void OnSprint()
        {
            stateMachine.SwitchState(new PlayerSprintingState(stateMachine));
        }

        private void OnJump()
        {
            stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
        }

        private void OnTarget()
        {
            if(!stateMachine.Targeter.SelectTarget()) { return; }
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }

        private void OnInteract()
        {
            if(stateMachine.Interactor.GetInteractableObject() != null)
            {
                stateMachine.SwitchState(new PlayerInteractingState(stateMachine));
            }
        }
    }
}

