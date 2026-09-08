using System;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrade
{
    [Serializable]
    public struct UpgradeData
    {
        public string Id;
        public string DisplayName;
        public int InitialValue;
        public double ValueMultiplier;
        public int InitialCost;
        public double CostMultiplier;
        [HideInInspector] public int CurrentLevel;
        public int MaxLevel;
        public List<string> Prerequisites;

        public UpgradeData(string id, string displayName, int maxLevel, int currentLevel, int initialValue = 1, int initialCost = 1, double costMultiplier = 1.5,
            double valueMultiplier = 1.5)
        {
            Id = id;
            DisplayName = displayName;
            InitialValue = initialValue;
            ValueMultiplier = valueMultiplier;
            InitialCost = initialCost;
            CostMultiplier = costMultiplier;
            CurrentLevel = 0;
            MaxLevel = maxLevel;
            Prerequisites = new List<string>();
        }
    }
}