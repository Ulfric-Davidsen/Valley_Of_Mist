using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.Player
{
    public class PlayerDodgingState : PlayerBaseState
    {
        private readonly int DodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
        private readonly int DodgeForwardHash = Animator.StringToHash("DodgeForward");
        private readonly int DodgeRightHash = Animator.StringToHash("DodgeRight");

        private const float CrossFadeDuration = 0.1f;

        private Vector3 dodgingDirectionInput;
        private float remainingDodgeTime;

        public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine)
        {
            this.dodgingDirectionInput = dodgingDirectionInput;
        }

        public override void Enter()
        {
            remainingDodgeTime = stateMachine.DodgeDuration;

            stateMachine.Animator.SetFloat(DodgeForwardHash, dodgingDirectionInput.y);
            stateMachine.Animator.SetFloat(DodgeRightHash, dodgingDirectionInput.x);
            stateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTreeHash, CrossFadeDuration);

            stateMachine.Health.SetInvulnerable(true);

            stateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTreeHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            Vector3 movement = new Vector3();

            movement += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.DodgeDistance / stateMachine.DodgeDuration;
            movement += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.DodgeDistance / stateMachine.DodgeDuration;

            Move(movement, deltaTime);

            FaceTarget();

            remainingDodgeTime -= deltaTime;

            if(remainingDodgeTime <= 0f)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
        }

        public override void Exit()
        {
            stateMachine.Health.SetInvulnerable(false);
        }
    }
}