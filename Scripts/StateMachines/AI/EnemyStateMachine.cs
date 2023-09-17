using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using WSS.StateMachines;
using WSS.Attributes;

namespace WSS.StateMachines.AI
{
    public class EnemyStateMachine : StateMachine
    {
        //Reference Assignment
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public Target Target { get; private set; }
        [field: SerializeField] public Ragdoll Ragdoll { get; private set; }

        //Movement Variables
        [field: SerializeField] public float MovementSpeed { get; private set; }

        //Range Variables
        [field: SerializeField] public float PlayerChasingRange { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }

        //Attack Array
        [field: SerializeField] public Attack[] Attacks { get; private set; }

        public Health Player { get; private set; }

        private void OnEnable()
        {
            Health.OnTakeDamage += HandleTakeDamage;
            Health.OnDie += HandleDeath;
        }

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

            NavMeshAgent.updatePosition = false;
            NavMeshAgent.updateRotation = false;

            SwitchState(new EnemyIdleState(this));
        }

        private void HandleTakeDamage()
        {
            SwitchState(new EnemyImpactState(this));
        }

        private void HandleDeath()
        {
            SwitchState(new EnemyDeadState(this));
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
        }

        private void OnDisable()
        {
            Health.OnTakeDamage -= HandleTakeDamage;
            Health.OnDie -= HandleDeath;
        }
    }
}