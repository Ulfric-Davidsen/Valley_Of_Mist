using UnityEngine;
using System.Collections.Generic;
using System;

namespace WSS.Stats
{
    [CreateAssetMenu(fileName = "LevelingTable", menuName = "Stats/New Leveling Table", order = 1)]
    public class LevelingTable : ScriptableObject
    {
        public int CharacterBaseLevel;
        public int CharacterCurrentLevel;
        public int CharacterMaxLevel;
        public int TotalExperience;
        public int ExperienceToNextLevel;
        public int[] ExperienceNeededForLevel;
    }
}
