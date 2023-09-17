using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.Player
{
    public class PlayerSprintingState : PlayerBaseState
    {
        private readonly int SprintHash = Animator.StringToHash("Sprint");

        private const float CrossFadeDuration = 0.1f;

        private float sprintMultiplier = 1.5f;

        public PlayerSprintingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            Debug.Log("Enter Sprint");
            stateMachine.InputReader.SprintEvent += OnSprint;
            stateMachine.Animator.CrossFadeInFixedTime(SprintHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            Vector3 movement = CalculateFreeLookMovementDirection();

            Move(movement * (stateMachine.FreeLookMovementSpeed * sprintMultiplier), deltaTime);

            if(stateMachine.InputReader.MovementValue == Vector2.zero)
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                return;
            }

            FaceMovementDirection(movement, deltaTime);
        }

        public override void Exit()
        {
            Debug.Log("Exit Sprint");
            stateMachine.InputReader.SprintEvent -= OnSprint;
        }

        private void OnSprint()
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}
