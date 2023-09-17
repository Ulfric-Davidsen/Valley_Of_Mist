using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.Player
{
    public class PlayerFallingState : PlayerBaseState
    {
        private readonly int LandHash = Animator.StringToHash("Land");

        private const float CrossFadeDuration = 0.1f;

        private Vector3 momentum;
        private float fallMomentum = -6f;

        public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            momentum = stateMachine.Controller.velocity;
            momentum.y = fallMomentum;

            stateMachine.Animator.CrossFadeInFixedTime(LandHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            Move(momentum, deltaTime);

            if(stateMachine.Controller.isGrounded)
            {
                ReturnToLocomotion();
            }

            FaceTarget();
        }

        public override void Exit()
        {

        }
    }
}
