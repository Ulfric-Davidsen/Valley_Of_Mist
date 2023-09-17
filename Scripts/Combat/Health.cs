using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.Stats;

namespace WSS.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private CharacterStats characterStats;
        [SerializeField] private CharacterClass characterClass;

        public event Action OnTakeDamage;
        public event Action OnDie;

        public bool IsDead => health == 0f;

        private float health;
        private bool isInvulnerable;
        
        void Start()
        {
            health = characterClass.MaxHealth;
        }

        private void Update()
        {
            RecordAttributeValue();
        }

        public void SetInvulnerable(bool isInvulnerable)
        {
            this.isInvulnerable = isInvulnerable;
        }

        public void TakeDamage(float damage)
        {
            if(health == 0) { return; }

            if(isInvulnerable) { return; }

            health = Mathf.Max(health - damage, 0);

            OnTakeDamage?.Invoke();

            if(health == 0)
            {
                OnDie?.Invoke();
            }

            Debug.Log(health);
        }

        private void RecordAttributeValue()
        {
            characterStats.CurrentHealth = health;
        }

    }
}
