using UnityEngine;
using System.Collections.Generic;
using System;

namespace WSS.Stats
{
    [CreateAssetMenu(fileName = "CharacterClass", menuName = "Stats/New Character Class", order = 0)]
    public class CharacterClass : ScriptableObject
    {
        [Header("Base Stats")]
        public int Vitality; //Responsible for max health and contributes to physical resistance
        public int Endurance; //Responsible for max stamina and contributes to poison resistance
        public int Willpower; //Responsible for max mana and contributes to magical resistance
        public int Accuracy; //Responsible for increasing the chances of a max or critical hit
        public int Strength; //Responsible for increasing the max hit of strength based weapons, as well as providing a level requirement
        public int Dexterity; //Responsible for increasing the max hit of dexterity based weapons, as well as providing a level requirement
        public int Intelligence; //Responsible for increasing the max hit of combat spells, as well as providing a level requirement
        public int Devotion; //Responsible for the effectiveness of blessings and holy magic, as well as providing a level requirement

        [Header("Attributes")]
        public float MaxHealth;
        public float MaxStamina;
        public float MaxMana;

        [Header("Attribute Multiplier")]
        public int AttributeMultiplier;
    }
}
