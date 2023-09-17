using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.Player
{
    public class PlayerInteractingState : PlayerBaseState
    {
        private float interactionDuration;
        // private readonly int InteractingHash = Animator.StringToHash("Interacting");

        // private const float CrossFadeDuration = 0.1f;

        public PlayerInteractingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.InputReader.InteractEvent += OnInteract;
            stateMachine.Interactor.Interact();
            interactionDuration = stateMachine.Interactor.GetCurrentInteractionDuration();
            Debug.Log("Player is interacting with object.");

            // stateMachine.Animator.CrossFadeInFixedTime(InteractingHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            if(!stateMachine.Interactor.InteractionHasDuration())
            {
                return;
            }
            else if (stateMachine.Interactor.InteractionHasDuration())
            {
                interactionDuration -= deltaTime;

                if(interactionDuration <= 0)
                {
                    stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                    return;
                }
            }
        }

        public override void Exit()
        {
            stateMachine.InputReader.InteractEvent -= OnInteract;
            Debug.Log("Player is done interacting.");
        }

        private void OnInteract()
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}