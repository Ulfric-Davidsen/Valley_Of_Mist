using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;
using WSS.Attributes;

namespace WSS.StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        //Reference Assignment
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Targeter Targeter { get; private set; }
        [field: SerializeField] public Interactor Interactor { get; private set; }
        [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
        [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public Ragdoll Ragdoll { get; private set; }

        //Movement Variables
        [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
        [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
        [field: SerializeField] public float RotationDamping { get; private set; }

        //Dodging Variables
        [field: SerializeField] public float DodgeDuration { get; private set; }
        [field: SerializeField] public float DodgeDistance { get; private set; }

        //Jumping Variables
        [field: SerializeField] public float JumpForce { get; private set; }

        //Attack Array
        [field: SerializeField] public Attack[] Attacks { get; private set; }

        public Transform MainCameraTransform { get; private set; }

        public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;

        private void OnEnable()
        {
            Health.OnTakeDamage += HandleTakeDamage;
            Health.OnDie += HandleDeath;
        }

        private void Start()
        {
            // Cursor.lockState = CursorLockMode.Locked;
            // Cursor.visible = false;

            MainCameraTransform = Camera.main.transform;
            
            SwitchState(new PlayerFreeLookState(this));
        }

        private void HandleTakeDamage()
        {
            SwitchState(new PlayerImpactState(this));
        }

        private void HandleDeath()
        {
            SwitchState(new PlayerDeadState(this));
        }

        private void OnDisable()
        {
            Health.OnTakeDamage -= HandleTakeDamage;
            Health.OnDie -= HandleDeath;
        }
    }
}

