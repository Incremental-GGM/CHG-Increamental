using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Upgrade;

namespace Manager
{
    public class UpgradeManager : MonoSingleton<UpgradeManager>
    {
        public event Action<string,  int> OnStatChanged;
        private Dictionary<string, UpgradeData> _upgradeDatas = new Dictionary<string, UpgradeData>();
        
        [SerializeField] private List<UpgradeData> _defaultUpgradeData;
        
        private void Start()
        {
            _upgradeDatas = _defaultUpgradeData.ToDictionary(k => k.Id, v =>
            {
                OnStatChanged?.Invoke(v.Id, v.InitialValue);
                return v;
            });
        }

        public bool TryUpgrade(string key)
        {
            if (_upgradeDatas.TryGetValue(key, out var data))
            {
                if (!GoldManager.Instance.TryBuy(data.InitialCost) || data.MaxLevel <= data.CurrentLevel) return false;
                if (data.Prerequisites != null)
                {
                    foreach (string prerequisite in data.Prerequisites)
                        if (!_upgradeDatas.ContainsKey(prerequisite) && _upgradeDatas[prerequisite].CurrentLevel <= 0) 
                            return false;
                    
                }

                data.InitialCost += (int)Math.Pow(data.CostMultiplier, data.CurrentLevel);
                data.CurrentLevel++;
                data.InitialValue += (int)Math.Pow(data.ValueMultiplier, data.CurrentLevel);
                
                
                OnStatChanged?.Invoke(data.Id, data.CurrentLevel);
                return true;
            }

            return false;
        }
        
        
    }
}