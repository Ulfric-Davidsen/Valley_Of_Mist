using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.Player
{
    public class PlayerImpactState : PlayerBaseState
    {
        private readonly int ImpactHash = Animator.StringToHash("Impact");

        private const float CrossFadeDuration = 0.1f;

        private float duration = 0.75f;

        public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine) { } 

        public override void Enter()
        {
            stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);

            duration -= deltaTime;

            if(duration <= 0f)
            {
                ReturnToLocomotion();
            }
        }

        public override void Exit()
        {
            
        }
    }
}
