using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;

namespace WSS.StateMachines.AI
{
    public class EnemyDeadState : EnemyBaseState
    {
        public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            if(stateMachine.NavMeshAgent.enabled == true)
            {
                stateMachine.NavMeshAgent.enabled = false;
            }
            
            stateMachine.Ragdoll.ToggleRagdoll(true);
            stateMachine.WeaponDamage.gameObject.SetActive(false);
            GameObject.Destroy(stateMachine.Target);
        }

        public override void Tick(float deltaTime)
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}