using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.AI
{
    public class EnemyAttackingState : EnemyBaseState
    {
        private Attack attack;

        private float previousFrameTime;

        private bool alreadyAppliedForce;

        private bool isAttacking;

        public EnemyAttackingState(EnemyStateMachine stateMachine, int attackIndex) : base(stateMachine)
        {
            attack = stateMachine.Attacks[attackIndex];
        }

        public override void Enter()
        {
            FacePlayer();

            if(stateMachine.NavMeshAgent.enabled == true)
            {
                stateMachine.NavMeshAgent.enabled = false;
            }
    
            isAttacking = true;

            stateMachine.WeaponDamage.SetAttack(attack.Damage, attack.Knockback);
            stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if(normalizedTime < 1f)
        {
                if(normalizedTime >= attack.ForceTime)
                {
                    TryApplyForce();
                }

                if(isAttacking)
                {
                    TryComboAttack(normalizedTime);
                }
        }
        else
        {
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
                return;
        }

        previousFrameTime = normalizedTime;
            
        }

        public override void Exit()
        {
            isAttacking = false;
        }

        private void TryComboAttack(float normalizedTime)
        {
            if(attack.ComboStateIndex == -1 || !IsInAttackRange()) { return; }

            if(normalizedTime < attack.ComboAttackTime) { return; }

            stateMachine.SwitchState(new EnemyAttackingState(stateMachine, attack.ComboStateIndex));
        }

        private void TryApplyForce()
        {
            if(alreadyAppliedForce) { return; }

            stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);

            alreadyAppliedForce = true;
        }

        private bool IsInAttackRange()
        {
            if(stateMachine.Player.IsDead) { return false; }
            
            float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

            return playerDistanceSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
        }

    }
}