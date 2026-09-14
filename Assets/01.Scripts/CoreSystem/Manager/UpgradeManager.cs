using System;
using System.Collections.Generic;
using System.Linq;
using _01.Scripts.Upgrade;
using Manager;
using UnityEngine;
using Upgrade;

namespace _01.Scripts.CoreSystem.Manager
{
    public class UpgradeManager : MonoSingleton<UpgradeManager>
    {
        public event Action<string, int> OnStatChanged;
        private Dictionary<string, UpgradeData> _upgradeDataDict = new Dictionary<string, UpgradeData>();
        
        [SerializeField] private List<UpgradeData> _defaultUpgradeData;

        protected override void Awake()
        {
            base.Awake();
            _upgradeDataDict = _defaultUpgradeData.ToDictionary(k => k.Id, v =>
            {
                OnStatChanged?.Invoke(v.Id, v.CurrentLevel);
                return v;
            });
        }

        public bool TryUpgrade(string key)
        {
            if (_upgradeDataDict.TryGetValue(key, out var data))
            {
                if (!GoldManager.Instance.TryBuy(data.CalculateCost(data.CurrentLevel)) || data.MaxLevel <= data.CurrentLevel) return false;
                if (data.Prerequisites != null) //선행 조건 있다면 체크
                {
                    foreach (string prerequisite in data.Prerequisites)
                        if (!_upgradeDataDict.ContainsKey(prerequisite) && _upgradeDataDict[prerequisite].CurrentLevel <= 0) 
                            return false;
                    
                }

                data.CurrentLevel++;
                data.InitialCost += Mathf.Pow(data.CostMultiplier, data.CurrentLevel);
                data.OnValueChanged(data);
				OnStatChanged?.Invoke(data.Id, data.CurrentLevel);
                return true;
            }

            return false;
        }

        public UpgradeData GetUpgradeData(string key)
        {
            if (_upgradeDataDict.TryGetValue(key, out var data))
                return data;
            else return new UpgradeData();
        }
    }
}