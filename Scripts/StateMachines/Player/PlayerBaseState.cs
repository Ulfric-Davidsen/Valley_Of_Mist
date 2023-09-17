using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using WSS.StateMachines;

namespace WSS.StateMachines.Player
{
    public abstract class PlayerBaseState : State
    {
        protected PlayerStateMachine stateMachine;

        public PlayerBaseState(PlayerStateMachine stateMachine)
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

        protected Vector3 CalculateFreeLookMovementDirection()
        {
            Vector3 forward = stateMachine.MainCameraTransform.forward;
            Vector3 right = stateMachine.MainCameraTransform.right;
            
            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;
        }

        protected void FaceMovementDirection(Vector3 movement, float deltaTime)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, 
                Quaternion.LookRotation(movement), 
                deltaTime * stateMachine.RotationDamping);
            
        }

        protected Vector3 CalculateTargetingMovement(float deltaTime)
        {
            Vector3 movement = new Vector3();

            movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

            return movement;
        }

        protected void FaceTarget()
        {
            if(stateMachine.Targeter.CurrentTarget == null) { return; }

            Vector3 lookPosition = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
            lookPosition.y = 0f;

            stateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
        }

        protected void ReturnToLocomotion()
        {
            if(stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }

        protected bool IsMouseOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}