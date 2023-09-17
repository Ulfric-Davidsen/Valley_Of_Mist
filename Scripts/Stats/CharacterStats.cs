using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSS.Stats
{
    public class CharacterStats : MonoBehaviour
    {
        [Header("Character Base Stats")]
        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private int vitality;
        [SerializeField] private int endurance;
        [SerializeField] private int willpower;
        [SerializeField] private int accuracy;
        [SerializeField] private int strength;
        [SerializeField] private int intelligence;
        [SerializeField] private int devotion;

        [Header("Character Attributes")]
        [SerializeField] private float maxHealth;
        [SerializeField] private float maxStamina;
        [SerializeField] private float maxMana;
        public float CurrentHealth;
        public float CurrentStamina;
        public float CurrentMana;

        [Header("Character Level and Experience")]
        [SerializeField] LevelingTable levelingTable = null;
        [SerializeField] private int characterCurrentLevel;
        [SerializeField] private int characterMaxLevel;
        [SerializeField] private int totalExperience;
        [SerializeField] private int experienceToNextLevel;

        [Header("Experience Needed For Level")]
        [SerializeField] private int experienceNeededForLevel;

        public event Action LevelUpEvent;

        private int expArrayValue;

        void Start()
        {
            SetStats();
            SetAttributes();

            if(levelingTable != null)
            {
                SetupLevelingTable();
            }
        }

        private void OnEnable()
        {
            LevelUpEvent += OnLevelUp;
        }

        private void Update()
        {
            if(levelingTable != null)
            {
                TrackExperience();
            }
        }

        private void SetStats()
        {
            vitality = characterClass.Vitality;
            endurance = characterClass.Endurance;
            willpower = characterClass.Willpower;
            accuracy = characterClass.Accuracy;
            strength = characterClass.Strength;
            intelligence = characterClass.Intelligence;
            devotion = characterClass.Devotion;
        }

        private void SetAttributes()
        {
            maxHealth = (float)characterClass.Vitality * characterClass.AttributeMultiplier;
            maxStamina = (float)characterClass.Endurance * characterClass.AttributeMultiplier;
            maxMana = (float)characterClass.Willpower * characterClass.AttributeMultiplier;

            RecordAttributeValues();
        }

        private void SetupLevelingTable()
        {
            if(characterCurrentLevel <= 0) { levelingTable.CharacterBaseLevel = 1; }

            if(levelingTable.CharacterCurrentLevel <= levelingTable.CharacterBaseLevel)
            {
                characterCurrentLevel = levelingTable.CharacterBaseLevel;
            }
            else
            {
                characterCurrentLevel = levelingTable.CharacterCurrentLevel;
            }
            
            characterMaxLevel = levelingTable.CharacterMaxLevel;
            totalExperience = levelingTable.TotalExperience;

            if(levelingTable.ExperienceNeededForLevel.Length != levelingTable.CharacterMaxLevel)
            {
                Debug.LogWarning("Set array length equal to CharacterMaxLevel");
            }

            expArrayValue = characterCurrentLevel - 1;
            experienceNeededForLevel = levelingTable.ExperienceNeededForLevel[expArrayValue];
        }

        private void OnLevelUp()
        {
            characterCurrentLevel++;

            int nextExpArrayValue = characterCurrentLevel - 1;

            experienceNeededForLevel = levelingTable.ExperienceNeededForLevel[nextExpArrayValue];

            RecordValuesToLevelingTable();
            RecordAttributeValues();
        }

        private void RecordValuesToLevelingTable()
        {
            levelingTable.CharacterCurrentLevel = characterCurrentLevel;
        }

        private void RecordAttributeValues()
        {
            characterClass.MaxHealth = maxHealth;
            characterClass.MaxStamina = maxStamina;
            characterClass.MaxMana = maxMana;
        }

        private void TrackExperience()
        {
            levelingTable.TotalExperience = totalExperience;
            levelingTable.ExperienceToNextLevel = experienceToNextLevel;
            
            if(characterCurrentLevel == characterMaxLevel)
            {
                experienceToNextLevel = 0;
                return;
            }
            else
            {
                experienceToNextLevel = experienceNeededForLevel - totalExperience;
            }

            if(experienceToNextLevel <= 0 && characterCurrentLevel != characterMaxLevel)
            {
                LevelUpEvent?.Invoke();
            }
        }

        private void OnDisable()
        {
            LevelUpEvent -= OnLevelUp;
        }
    }
}
